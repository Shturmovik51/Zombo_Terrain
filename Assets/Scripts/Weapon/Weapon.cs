using System;
using System.Collections;
using UnityEngine;

namespace ZomboTerrain
{
    public class Weapon : IWeapon
    {
        public event Action<bool> OnWeaponShoot;
        public event Action<int, int> OnAmmoChange;
        public event Action OnEmptyAmmo;

        protected int _maxMagazineAmmo;
        protected int _ammoMagazineCount;
        protected bool _isShootDelay;
        protected bool _isReloading;
        protected float _shootDelayTime = 0.1f;
        protected float _reloadTime;
        protected Light _flashLight;
        protected Camera _mainCamera;
        protected GameManager _gameManager;
        protected Transform _weaponPosition;
        protected Transform _flashLightPosition;
        protected Transform _shootEffectPosition;

        private Coroutine _shootDelay;

        public virtual void Shoot(int ammoCount)
        {
            _ammoMagazineCount--;

            if (_ammoMagazineCount < 0)
            {
                _ammoMagazineCount = 0;
                OnEmptyAmmo?.Invoke();
                return;
            }

            _isShootDelay = true;
            OnAmmoChange?.Invoke(ammoCount, _ammoMagazineCount);
            OnWeaponShoot?.Invoke(_isShootDelay);

            if (_shootDelay == null)
                _shootDelay = _gameManager.StartCoroutine(ShootDelay(_shootDelayTime));
        }

        public void ReloadWeapon(int ammoCount)
        {
            if (ammoCount == 0)
                return;
            _isReloading = true;
            _gameManager.StartCoroutine(ReloadTimer(ammoCount));
        }

        private IEnumerator ReloadTimer(int ammoCount)
        {
            yield return new WaitForSeconds(_reloadTime);

            var ammoNeeded = _maxMagazineAmmo - _ammoMagazineCount;

            if (ammoCount <= ammoNeeded)
            {
                _ammoMagazineCount = ammoCount;
                ammoCount = 0;
            }
            else
            {
                _ammoMagazineCount = _maxMagazineAmmo;
                ammoCount -= ammoNeeded;
            }

            OnAmmoChange?.Invoke(ammoCount, _ammoMagazineCount);
            _isReloading = false;
        }

        private IEnumerator ShootDelay(float shootDelayTime)
        {
            yield return new WaitForSeconds(shootDelayTime);
            _isShootDelay = false;
            OnWeaponShoot?.Invoke(_isShootDelay);
            _shootDelay = null;
            yield break;
        }

        public void FlashLightOnOff()
        {
            _flashLight.enabled = !_flashLight.enabled;
        }
    }
}
