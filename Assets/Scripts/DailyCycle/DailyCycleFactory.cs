namespace ZomboTerrain
{
    public sealed class DailyCycleFactory
    {
        private readonly DailyCycleData _data;

        public DailyCycleFactory(DailyCycleData dailyCycleData)
        {
            _data = dailyCycleData;
        }

        public DailyCycleModel CreateDailyCycleModel()
        {
            return new DailyCycleModel(_data.CloudColorChangeSpeed, _data.SunSetCloudColor, _data.DayCloudColor,
                                        _data.CloudsMaterial);
        }
    }
}