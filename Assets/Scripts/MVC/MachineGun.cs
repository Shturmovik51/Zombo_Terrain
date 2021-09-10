using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    private int shootDamage;
    private int hitImpulseForce;
    private float lightEffectsDelayTime;
    private GameObject shootEffects;
    private Coroutine wfxHitLifeTime;
    private Coroutine lightEffectsDelay;
    public MachineGun(int maxMagazineAmmo, int shootDamage, int hitImpulseForce, float lightEffectsDelayTime, float reloadTime,
                        GameObject shootEffects, Light flashLight)
    {
        this.maxMagazineAmmo = maxMagazineAmmo;
        this.shootDamage = shootDamage;
        this.hitImpulseForce = hitImpulseForce;
        this.lightEffectsDelayTime = lightEffectsDelayTime;
        this.reloadTime = reloadTime;
        this.shootEffects = shootEffects;
        this.flashLight = flashLight;
    }
    public override void Shoot(int ammoCount)
    {
        if (isShootDelay || isReloading)
            return;

        base.Shoot(ammoCount);

        if (ammoMagazineCount == 0)
            return;

        RaycastHit hit;        
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                var hitedGO = hit.collider.transform.root.gameObject;

                if (GameManager.instance.HealthContainer.ContainsKey(hitedGO))
                {
                    var targethealth = GameManager.instance.HealthContainer[hitedGO];
                    targethealth.TakeDamage(shootDamage);
                }

                TakeWFXhitFromPool(GameManager.instance.WfxBodyHits, hit);

                var hitRigidBody = hit.collider.GetComponent<Rigidbody>();
                hitRigidBody.AddForce(Camera.main.transform.forward * hitImpulseForce, ForceMode.Impulse);
            }
            else
            {
                TakeWFXhitFromPool(GameManager.instance.WfxSandHits, hit);
            }

        LightEffectsOn();
    }

    private void TakeWFXhitFromPool(List<GameObject> wfxPool, RaycastHit hit)
    {
        var wfxHit = wfxPool[0];
        wfxPool.Remove(wfxHit);
        wfxHit.transform.position = hit.point;
        wfxHit.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        wfxHit.SetActive(true);
        wfxHitLifeTime = GameManager.instance.StartCoroutine(WFXhitLifeTime(wfxPool, wfxHit));
    }

    private void ReturnWFXhitToPool(List<GameObject> wfxPool, GameObject wfxHit)
    {
        wfxHit.SetActive(false);
        wfxHit.transform.rotation = Quaternion.identity;
        wfxPool.Add(wfxHit);
    }

    private IEnumerator WFXhitLifeTime(List<GameObject> wfxPool, GameObject wfxHit)
    {
        yield return new WaitForSeconds(10);
        ReturnWFXhitToPool(wfxPool, wfxHit);
    }
    private void LightEffectsOn()
    {
        shootEffects.transform.Rotate(Vector3.forward, Random.Range(0f, 360f), Space.Self);        
        shootEffects.SetActive(true);
        
        lightEffectsDelay = GameManager.instance.StartCoroutine(LightEffectsDelay(lightEffectsDelayTime));
    }
    public IEnumerator LightEffectsDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);        
        shootEffects.SetActive(false);        
        yield break;
    }
}
