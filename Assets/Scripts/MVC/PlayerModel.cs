using System.Collections;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel
{
    public UnityAction<int, int, Vector3> PlayerMoving;
    public UnityAction<bool> PlayerShooting;
    public UnityAction PlayerJumping;
    public UnityAction<float, float> PlayerLooking;
    public UnityAction PlayerReloading;
    public UnityAction<int, int> AmmoChaging;

    private Weapon weapon;
    private int moveSpeed;
    private int axeleration;
    private int ammoCount = 100;
    private float verticalRotation;
    public int Axeleration { get => axeleration; set => axeleration = value; }

    public PlayerModel(int moveSpeed, Weapon weapon)
    {
        this.moveSpeed = moveSpeed;
        this.weapon = weapon;
    }

    public void InitWeapon()
    {
        weapon.weaponShoot += PLayerShootAnim;
        weapon.ammoChange += AmmoCountChange;
    }

    public void PlayerMove(float xMoveDir, float zMoveDir, Vector3 xMovement, Vector3 zMovement)
    {   
        var moveDirection = (xMoveDir * xMovement + zMoveDir * zMovement);
        moveDirection *= axeleration;
        
        PlayerMoving?.Invoke(moveSpeed, axeleration, moveDirection);
    }

    public void PLayerShoot()
    {  
        weapon.Shoot(ammoCount);
    }

    private void PLayerShootAnim(bool isShootDelay)
    {
        PlayerShooting?.Invoke(isShootDelay);
    }

    public void PlayerJump()
    {

    }

    public void PlayerLook(float mouseLookX, float mouseLookY)
    {
        verticalRotation -= mouseLookY;
        verticalRotation = Mathf.Clamp(verticalRotation, -45f, 45f);
        PlayerLooking?.Invoke(mouseLookX, verticalRotation);
    }

    public void PLayerReloadWeapon()
    {
        weapon.ReloadWeapon(ammoCount);
        PlayerReloading?.Invoke();
    }

    private void AmmoCountChange(int ammoCount, int ammoMagazineCount)
    {
        AmmoChaging?.Invoke(ammoCount, ammoMagazineCount);
    }


}
