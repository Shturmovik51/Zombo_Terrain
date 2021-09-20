using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon
{
    public UnityAction<bool> OnWeaponShoot;
    public UnityAction<int, int> OnAmmoChange;
    public UnityAction OnEmptyAmmo;

    public int maxMagazineAmmo;
    public int ammoMagazineCount;
    public float shootDelayTime = 0.1f;
    public float reloadTime;
    public bool isShootDelay;
    public Light flashLight;
    private Coroutine _shootDelay;
    public bool isReloading;

    public GameManager gameManager;

    public virtual void Shoot(int ammoCount)
    {
        ammoMagazineCount--;

        if (ammoMagazineCount < 0)
        {
            ammoMagazineCount = 0;
            OnEmptyAmmo?.Invoke();
            return;
        }

        isShootDelay = true;
        OnAmmoChange?.Invoke(ammoCount, ammoMagazineCount);
        OnWeaponShoot?.Invoke(isShootDelay);

        if (_shootDelay == null)
            _shootDelay = gameManager.StartCoroutine(ShootDelay(shootDelayTime));
    }

    public void ReloadWeapon(int ammoCount)
    {
        if (ammoCount == 0)
            return;
        isReloading = true;
        gameManager.StartCoroutine(ReloadTimer(ammoCount));
    }

    private IEnumerator ReloadTimer(int ammoCount)
    {
        yield return new WaitForSeconds(reloadTime);

        var ammoNeeded = maxMagazineAmmo - ammoMagazineCount; 

        if (ammoCount <= ammoNeeded)
        {
            ammoMagazineCount = ammoCount;
            ammoCount = 0;
        }   
        else
        {
            ammoMagazineCount = maxMagazineAmmo;
            ammoCount -= ammoNeeded;
        }          
        
        OnAmmoChange?.Invoke(ammoCount, ammoMagazineCount);
        isReloading = false;
    }

    private IEnumerator ShootDelay(float shootDelayTime)
    {
        yield return new WaitForSeconds(shootDelayTime);       
        isShootDelay = false;
        OnWeaponShoot?.Invoke(isShootDelay);
        _shootDelay = null;
        yield break;
    }

    public void FlashLightOnOff()
    {
        flashLight.enabled = !flashLight.enabled;
    }
}
