using UnityEngine;

namespace ZomboTerrain
{
    public interface IDailyCycleModel
    {
        public float CloudColorChangeSpeed { get; }
        public Color SunSetCloudColor { get; }
        public Color DayCloudColor { get; }
        public Color NightCloudColor { get; }
        public Color SunRiseCloudColor { get; }
        public Material CloudsMaterial { get; }
        public Light SunLight { get; }
    }
}
