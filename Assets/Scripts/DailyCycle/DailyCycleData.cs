using UnityEngine;

namespace ZomboTerrain
{
    [CreateAssetMenu(fileName = "DailyCycle Model", menuName = "Database / DailyCycle")]
    public sealed class DailyCycleData : ScriptableObject
    {
        [SerializeField] private float _cloudColorChangeSpeed;
        [SerializeField] private Color _sunSetCloudColor;
        [SerializeField] private Color _dayCloudColor;
        [SerializeField] private Color _nightCloudColor;
        [SerializeField] private Color _sunRiseCloudColor;
        [SerializeField] private Material _cloudsMaterial;

        public float CloudColorChangeSpeed => _cloudColorChangeSpeed;
        public Color SunSetCloudColor => _sunSetCloudColor;
        public Color DayCloudColor => _dayCloudColor;
        public Color NightCloudColor => _nightCloudColor;
        public Color SunRiseCloudColor => _sunRiseCloudColor;
        public Material CloudsMaterial => _cloudsMaterial;
    }
}
