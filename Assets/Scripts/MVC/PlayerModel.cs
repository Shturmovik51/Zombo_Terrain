using System.Collections;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel
{
    public UnityAction<int, int, Vector3> PositionChange;
    public UnityAction<bool> PlayerShooting;

    private Weapon weapon;
    private int moveSpeed;
    private int axeleration;
    private int ammoCount = 100;
    public int Axeleration { get => axeleration; set => axeleration = value; }

    public PlayerModel(int moveSpeed, Weapon weapon)
    {
        this.moveSpeed = moveSpeed;
        this.weapon = weapon;
    }

    public void InitWeapon()
    {
        weapon.weaponShoot += PLayerShootAnim;
    }

    public void PlayerMove(float xMoveDir, float zMoveDir, Vector3 xMovement, Vector3 zMovement)
    {   
        var moveDirection = (xMoveDir * xMovement + zMoveDir * zMovement);
        moveDirection *= axeleration;
        
        PositionChange?.Invoke(moveSpeed, axeleration, moveDirection);
    }

    public void PLayerShoot()
    {  
        weapon.Shoot(ammoCount);
    }

    private void PLayerShootAnim(bool isShootDelay)
    {
        PlayerShooting?.Invoke(isShootDelay);
    }


}
