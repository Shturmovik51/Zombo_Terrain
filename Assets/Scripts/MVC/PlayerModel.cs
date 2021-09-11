using System.Collections;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel
{
    public UnityAction<int, int, Vector3> PlayerMoving;
    public UnityAction<bool> PlayerShooting;
    public UnityAction<int> PlayerJumping;
    public UnityAction<float, float> PlayerLooking;
    public UnityAction PlayerReloading;
    public UnityAction<int, int> AmmoChanging;

    private Weapon weapon;
    private int moveSpeed;
    private int axeleration;
    private int ammoCount = 150;
    private float verticalRotation;
    private bool isGrounded;
    private Transform groundDetector;
    private LayerMask groundMask;
    private int jumpForce;
    public int Axeleration { get => axeleration; set => axeleration = value; }

    public PlayerModel(int moveSpeed, int jumpForce, Weapon weapon, Transform groundDetector, LayerMask groundMask)
    {
        this.moveSpeed = moveSpeed;
        this.jumpForce = jumpForce;
        this.weapon = weapon;
        this.groundDetector = groundDetector;
        this.groundMask = groundMask;
    }

    public void InitWeapon()
    {
        weapon.weaponShoot += PLayerShootAnim;
        weapon.ammoChange += AmmoCountChange;
        weapon.emptyAmmo += PLayerReloadWeapon;        
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

    public void GroundDetection()
    {
        isGrounded = Physics.CheckSphere(groundDetector.position, 0.3f, groundMask);
    }

    private void PLayerShootAnim(bool isShootDelay)
    {
        PlayerShooting?.Invoke(isShootDelay);
    }

    public void PlayerJump()
    {
        if (!isGrounded)
            return;

        PlayerJumping?.Invoke(jumpForce);
    }

    public void PlayerLook(float mouseLookX, float mouseLookY)
    {
        verticalRotation -= mouseLookY;
        verticalRotation = Mathf.Clamp(verticalRotation, -45f, 45f);
        PlayerLooking?.Invoke(mouseLookX, verticalRotation);
    }

    public void PLayerReloadWeapon()
    {
        if (ammoCount == 0)
            return;
        weapon.ReloadWeapon(ammoCount);
        PlayerReloading?.Invoke();
    }

    private void AmmoCountChange(int ammoCount, int ammoMagazineCount)
    {
        this.ammoCount = ammoCount;
        AmmoChanging?.Invoke(ammoCount, ammoMagazineCount);
    }

    

}
