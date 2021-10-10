using UnityEngine;

namespace ZomboTerrain
{
    [CreateAssetMenu(fileName = "New Data", menuName = "Database / Data")]
    public class Data : ScriptableObject
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private InputKeysData _inputKeysData;
        [SerializeField] private DailyCycleData _dailyCycleData;
        [SerializeField] private WeaponModelData _weaponModelData;
        [SerializeField] private HitEffectsData _hitEffectsData;

        public PlayerData PlayerData => _playerData;
        public InputKeysData InputKeysData => _inputKeysData;
        public DailyCycleData DailyCycleData => _dailyCycleData;
        public WeaponModelData WeaponModelData => _weaponModelData;
        public HitEffectsData HitEffectsData => _hitEffectsData;

    }
}
