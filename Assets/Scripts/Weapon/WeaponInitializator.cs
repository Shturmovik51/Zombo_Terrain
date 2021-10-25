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
        private Light _flashLight;
        private InputController _inputController;
        //private List<Weapon> _weapons;
        public WeaponInitializator(Data data, float reloadTime, GameManager gameManager, Camera camera,
                                    HitEffectsController hitEffectsController, WeaponElements weaponElements,
                                        InputController inputController) 
        {
            _data = data;
            _reloadTime = reloadTime;
            _gameManager = gameManager;
            _camera = camera;
            _hitEffectsController = hitEffectsController;
            _inputController = inputController;
            _shootEffects = weaponElements.ShootEffects;
            _flashLight = weaponElements.FlashLight;
        }
        public Weapon InitWeapon()
        {
            WeaponModelData machineGunModel = _data.WeaponModelsData[0];
            Weapon machineGun = new MachineGun(machineGunModel.MaxMagazineAmmoCount, machineGunModel.ShootDamage,
                    machineGunModel.HitImpulseForce, machineGunModel.ShootFlashTime, _reloadTime, _shootEffects,
                    _flashLight, _gameManager, _camera, _hitEffectsController);

            _inputController.OnClickFlashLightButton += machineGun.FlashLightOnOff;

            return machineGun;
        }
    }
}
