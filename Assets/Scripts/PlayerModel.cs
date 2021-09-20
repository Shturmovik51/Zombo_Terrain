using System.Collections;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : ILiveEntity
{    
    private Weapon _playerWeapon;
    private int _moveSpeed;
    private int _axeleration;
    private int _ammoCount;
    private float _verticalRotation;    
    private int _jumpForce;
    private int _health;

    public int Axeleration { get => _axeleration; set => _axeleration = value; }
    public int MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public int AmmoCount { get => _ammoCount; set => _ammoCount = value; }
    public Weapon PlayerWeapon { get => _playerWeapon; set => _playerWeapon = value; }    
    public int JumpForce { get => _jumpForce; set => _jumpForce = value; }
    public float VerticalRotation { get => _verticalRotation; set => _verticalRotation = value; }
    public int Health { get => _health; set => _health = value; }

    public PlayerModel(int moveSpeed, int jumpForce, Weapon weapon, int ammoCount, int axeleration)
    {
        _moveSpeed = moveSpeed;
        _jumpForce = jumpForce;
        _playerWeapon = weapon;
        _ammoCount = ammoCount;
        _axeleration = axeleration;
    }

}
