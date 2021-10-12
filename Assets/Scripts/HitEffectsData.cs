using UnityEngine;

namespace ZomboTerrain
{
    [CreateAssetMenu(fileName = "New HitEffectsData", menuName = "Database / HitEffectsData")]
    public class HitEffectsData : ScriptableObject
    {
        [SerializeField] private GameObject _bodyHitEffect;
        [SerializeField] private GameObject _sandHitEffect;

        public GameObject BodyHitEffect => _bodyHitEffect;
        public GameObject SandHitEffect => _sandHitEffect;
    }
}
