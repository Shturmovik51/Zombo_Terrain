using System.Collections.Generic;
using UnityEngine;

namespace ZomboTerrain
{
    [CreateAssetMenu(fileName = "New Data", menuName = "Database / Data")]
    public class Data : ScriptableObject
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private InputKeysData _inputKeysData;
        [SerializeField] private DailyCycleData _dailyCycleData;
        [SerializeField] private List<WeaponModelData> _weaponModelsData;
        [SerializeField] private HitEffectsData _hitEffectsData;

        public PlayerData PlayerData => _playerData;
        public InputKeysData InputKeysData => _inputKeysData;
        public DailyCycleData DailyCycleData => _dailyCycleData;
        public List<WeaponModelData> WeaponModelsData => _weaponModelsData;
        public HitEffectsData HitEffectsData => _hitEffectsData;

    }
}
