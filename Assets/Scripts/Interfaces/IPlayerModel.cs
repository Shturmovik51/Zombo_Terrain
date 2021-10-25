namespace ZomboTerrain
{
    public interface IPlayerModel
    {
        public int Health { get; }
        public int JumpForce { get; }
        public int MoveSpeed { get; }
        public int AmmoCount { get; }
        public int Axeleration { get; }
        public float VerticalRotation { get; }
        public Weapon PlayerWeapon { get; }
    }
}
