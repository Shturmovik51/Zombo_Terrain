using UnityEngine;

namespace ZomboTerrain
{
    public class DailyCycleModel : IDailyCycleModel
    {
        public float CloudColorChangeSpeed { get; }
        public Color SunSetCloudColor { get; }
        public Color DayCloudColor { get; }
        public Material CloudsMaterial { get; }
        public float SunRotationPerMinute { get; }
        public DailyCycleModel(float cloudColorChangeSpeed, Color sunSetCloudColor, Color dayCloudColor, 
                                Material cloudsMaterial)
        {
            CloudColorChangeSpeed = cloudColorChangeSpeed;
            SunSetCloudColor = sunSetCloudColor;
            DayCloudColor = dayCloudColor;
            CloudsMaterial = cloudsMaterial;
            SunRotationPerMinute = 0.25f;
        }
    }
}
