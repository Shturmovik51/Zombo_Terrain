namespace ZomboTerrain
{
    public interface IOnSceneObject
    {
        public bool IsActive { get; }
        public void ObjectActivation(bool condition);
        public RadarController ObjectRadarController { get; set; }
    }
}