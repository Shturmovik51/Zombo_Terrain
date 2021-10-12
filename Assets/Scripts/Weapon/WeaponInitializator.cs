using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZomboTerrain
{
    public class WeaponInitializator
    {
        private Data _data;
        private float _reloadTime;
        private GameManager _gameManager;
        private Camera _camera;
        private HitEffectsController _hitEffectsController;
        private GameObject _shootEffects;
        //private List<Weapon> _weapons;
        public WeaponInitializator(Data data, float reloadTime, GameManager gameManager, Camera camera, 
                                    HitEffectsController hitEffectsController, GameObject shootEffects) 
        {
            _data = data;
            _reloadTime = reloadTime;
            _gameManager = gameManager;
            _camera = camera;
            _hitEffectsController = hitEffectsController;
            _shootEffects = shootEffects;
        }
        public Weapon InitWeapon()
        {
            WeaponModelData machineGunModel = _data.WeaponModelsData[0];
            Weapon machineGun = new MachineGun(machineGunModel.MaxMagazineAmmoCount, machineGunModel.ShootDamage,
                    machineGunModel.HitImpulseForce, machineGunModel.ShootFlashTime, _reloadTime, _shootEffects,
                    machineGunModel.FlashLight, _gameManager, _camera, _hitEffectsController);

            return machineGun;
        }
    }
}
