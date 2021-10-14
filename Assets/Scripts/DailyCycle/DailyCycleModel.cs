using UnityEngine;

namespace ZomboTerrain
{
    public sealed class DailyCycleModel : IDailyCycleModel
    {
        public float CloudColorChangeSpeed { get; }
        public Color SunSetCloudColor { get; }
        public Color DayCloudColor { get; }
        public Color NightCloudColor { get; }
        public Color SunRiseCloudColor { get; }
        public Material CloudsMaterial { get; set; }
        public float SunRotationPerMinute { get; }
        public Light SunLight { get; }
        public DailyCycleModel(float cloudColorChangeSpeed, Color sunSetCloudColor, Color dayCloudColor, Color nightCloudColor,
                                Color sunRiseCloudColor, Material cloudsMaterial, Light sunlight)
        {
            CloudColorChangeSpeed = cloudColorChangeSpeed;
            SunSetCloudColor = sunSetCloudColor;
            DayCloudColor = dayCloudColor;
            NightCloudColor = nightCloudColor;
            SunRiseCloudColor = sunRiseCloudColor;
            CloudsMaterial = cloudsMaterial;
            SunRotationPerMinute = 0.25f;
            SunLight = sunlight;
        }
    }
}
