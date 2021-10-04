using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Player Model", menuName = "Database / Model")]
public class PlayerModelContainer : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private int _moveSpeed;
    [SerializeField] private int _ammoCount;
    [SerializeField] private int _axeleration;
    [SerializeField] private int _jumpForce;
    [SerializeField] private float _verticalRotation;
    [SerializeField] private Weapon _playerWeapon;

    public int Health => _health;
    public int MoveSpeed => _moveSpeed;
    public int AmmoCount => _ammoCount;
    public int Axeleration => _axeleration;
    public int JumpForce => _jumpForce;
    public float VerticalRotation => _verticalRotation;
    public Weapon PlayerWeapon => _playerWeapon;
}