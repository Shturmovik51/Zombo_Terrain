using UnityEngine;

namespace ZomboTerrain
{
    [CreateAssetMenu(fileName = "Player Model", menuName = "Database / Model")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private int _moveSpeed;
        [SerializeField] private int _ammoCount;
        [SerializeField] private int _axeleration;
        [SerializeField] private int _jumpForce;
        [SerializeField] private Weapon _playerWeapon;

        public int Health => _health;
        public int MoveSpeed => _moveSpeed;
        public int AmmoCount => _ammoCount;
        public int Axeleration => _axeleration;
        public int JumpForce => _jumpForce;
        public Weapon PlayerWeapon => _playerWeapon;
    }
}