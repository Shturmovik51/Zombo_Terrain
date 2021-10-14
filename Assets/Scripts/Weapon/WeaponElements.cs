using UnityEngine;

namespace ZomboTerrain
{
    [System.Serializable]
    public struct WeaponElements
    {
        [SerializeField] private GameObject _shootEffects;
        [SerializeField] private Light _flashLight;
        [SerializeField] private Transform _weaponPosition;
        [SerializeField] private Transform _flashLightPosition;
        [SerializeField] private Transform _shootEffectPosition;

        public GameObject ShootEffects => _shootEffects;
        public Light FlashLight => _flashLight;
        public Transform WeaponPosition => _weaponPosition;
        public Transform FlashLightPosition => _flashLightPosition;
        public Transform ShootEffectPosition => _shootEffectPosition;
    }
}
