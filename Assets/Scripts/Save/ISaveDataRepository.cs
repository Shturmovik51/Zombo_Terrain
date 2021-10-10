namespace ZomboTerrain
{
    public interface ISaveDataRepository
    {
        void Save();
        void Load(/*PlayerModel player*/);
    }
}