using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZomboTerrain
{
    public class MachineGun : Weapon
    {
        private int _shootDamage;
        private int _hitImpulseForce;
        private float _lightEffectsDelayTime;
        private GameObject _shootEffects;
        private Coroutine _hitEffectLifeTime;
        private Coroutine _lightEffectsDelay;

        public MachineGun(int maxMagazineAmmo, int shootDamage, int hitImpulseForce, float lightEffectsDelayTime, float reloadTime,
                            GameObject shootEffects, Light flashLight, GameManager gameManager, Camera mainCamera, 
                            Transform weaponPosition, Transform flashLightPosition, Transform shootEffectPosition)
        {
            this.maxMagazineAmmo = maxMagazineAmmo;
            _shootDamage = shootDamage;
            _hitImpulseForce = hitImpulseForce;
            _lightEffectsDelayTime = lightEffectsDelayTime;
            this.reloadTime = reloadTime;
            _shootEffects = shootEffects;
            this.flashLight = flashLight;
            this.gameManager = gameManager;
            this.mainCamera = mainCamera;
        }
        public override void Shoot(int ammoCount)
        {
            if (isShootDelay || isReloading)
                return;

            base.Shoot(ammoCount);

            if (ammoMagazineCount == 0)
                return;

            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                var MainGO = hit.collider.transform.root.gameObject;

                if (MainGO.TryGetComponent(out ILiveEntity liveEntity))
                {
                    if (liveEntity is ITakeDamage enemy)
                    {
                        enemy.AddDamage(_shootDamage);
                    }

                    //TakeHitEffectFromPool(gameManager.BodyHitEffects, hit);

                    var hitRigidBody = hit.collider.GetComponent<Rigidbody>();
                    hitRigidBody.AddForce(Camera.main.transform.forward * _hitImpulseForce, ForceMode.Impulse);
                }
                else
                {
                    //TakeHitEffectFromPool(gameManager.SandHitEffects, hit);
                }
            }

            LightEffectsOn();
        }

        private void TakeHitEffectFromPool(List<GameObject> wfxPool, RaycastHit hit)
        {
            var wfxHit = wfxPool[0];
            wfxPool.Remove(wfxHit);
            wfxHit.transform.position = hit.point;
            wfxHit.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            wfxHit.SetActive(true);
            _hitEffectLifeTime = gameManager.StartCoroutine(WFXhitLifeTime(wfxPool, wfxHit));
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
            _shootEffects.transform.Rotate(Vector3.forward, Random.Range(0f, 360f), Space.Self);
            _shootEffects.SetActive(true);

            _lightEffectsDelay = gameManager.StartCoroutine(LightEffectsDelay(_lightEffectsDelayTime));
        }
        public IEnumerator LightEffectsDelay(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            _shootEffects.SetActive(false);
            yield break;
        }
    }
}
