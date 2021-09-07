using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    private int shootDamage;
    private int hitImpulseForce;
    public MachineGun(int maxMagazineAmmo, int shootDamage, int hitImpulseForce, GameObject shootEffects)
    {
        this.maxMagazineAmmo = maxMagazineAmmo;
        this.shootDamage = shootDamage;
        this.hitImpulseForce = hitImpulseForce;
        this.shootEffects = shootEffects;
    }
    public override void Shoot(int ammoCount)
    {
        if (isShootDelay == true)
            return;

        base.Shoot(ammoCount);

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

                TakeWFXhit(GameManager.instance.WfxBodyHits, hit);

                var hitRigidBody = hit.collider.GetComponent<Rigidbody>();
                hitRigidBody.AddForce(Camera.main.transform.forward * hitImpulseForce, ForceMode.Impulse);
            }
            else
            {
                TakeWFXhit(GameManager.instance.WfxSandHits, hit);
            }
    }

    private GameObject TakeWFXhit(List<GameObject> wfxPool, RaycastHit hit)
    {
        var wfxHit = wfxPool[0];
        wfxPool.Remove(wfxHit);
        wfxHit.transform.position = hit.point;
        wfxHit.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        wfxHit.SetActive(true);
        return wfxHit;
    }
}
