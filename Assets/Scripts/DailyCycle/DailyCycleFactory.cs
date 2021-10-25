using UnityEngine;

namespace ZomboTerrain
{
    public sealed class DailyCycleFactory
    {
        private readonly DailyCycleData _data;
        private readonly Light _sunLight;

        public DailyCycleFactory(DailyCycleData dailyCycleData, Light sunLight)
        {
            _data = dailyCycleData;
            _sunLight = sunLight;
        }

        public DailyCycleModel CreateDailyCycleModel()
        {
            return new DailyCycleModel(_data.CloudColorChangeSpeed, _data.SunSetCloudColor, _data.DayCloudColor,
                                        _data.NightCloudColor, _data.SunRiseCloudColor, _data.CloudsMaterial, _sunLight);
        }
    }
}