public class PlayerModel : IPlayerModel
{
    public int Health { get; set; }
    public int MoveSpeed { get; set; }
    public int AmmoCount { get; set; }
    public int Axeleration { get; set; }
    public int JumpForce { get; set; }
    public float VerticalRotation { get; set; }
    public Weapon PlayerWeapon { get; set; }

    public PlayerModel(int health, int moveSpeed, int jumpForce, Weapon weapon, int ammoCount, int axeleration)
    {
        Health = health;
        MoveSpeed = moveSpeed;
        AmmoCount = ammoCount;
        Axeleration = axeleration;
        JumpForce = jumpForce;
        PlayerWeapon = weapon;
    }

}
