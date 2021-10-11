using UnityEngine;

namespace ZomboTerrain
{
    public interface IDailyCycleModel
    {
        public float CloudColorChangeSpeed { get; }
        public Color SunSetCloudColor { get; }
        public Color DayCloudColor { get; }
        public Material CloudsMaterial { get; }
    }
}
