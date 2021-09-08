using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon
{
    public UnityAction<bool> weaponShoot;

    public int maxMagazineAmmo;
    public int ammoMagazineCount;
    public float shootDelayTime = 0.1f;

    public bool isShootDelay;
    private Coroutine shootDelay;

    public virtual void Shoot(int ammoCount)
    {
       
        ////ammoMagazineCount--;

        ////if (ammoMagazineCount < 0)
        ////{
        ////    ammoMagazineCount = 0;
        ////    Reload(ammoCount);
        ////    return;
        ////}
        
        //if (isShootDelay == true)
        //    return;

        isShootDelay = true;
        weaponShoot?.Invoke(isShootDelay);        

        if (shootDelay == null)
            shootDelay = GameManager.instance.StartCoroutine(ShootDelay(shootDelayTime));
    }

    public void Reload(int ammoCount)
    {

    }    

    public IEnumerator ShootDelay(float shootDelayTime)
    {
        yield return new WaitForSeconds(shootDelayTime);       
        isShootDelay = false;
        weaponShoot?.Invoke(isShootDelay);
        shootDelay = null;
        yield break;
    }


}
