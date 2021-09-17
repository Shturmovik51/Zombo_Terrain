using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : IDisposable
{
    private PlayerView playerView;
    private PlayerModel playerModel;

    public UnityAction<bool> OnGroundedStateChange;
    public PlayerController(PlayerView view, PlayerModel model)
    {
        playerView = view;
        playerModel = model;
    }

    public void Enable()
    {
        playerModel.PlayerMoving += PositionChange;
        playerModel.PlayerLooking += DirectionChange;
        playerModel.PlayerShooting += PlayerShootAnim;
        playerModel.PlayerJumping += PlayerJump;
        playerModel.PlayerReloading += PlayerReloadAnim;
        playerModel.AmmoChanging += RefreshAmmoUI;

        playerView.OnGroundDetectionState += GroundDetector;
    }

    public void Dispose()
    {
        playerModel.PlayerMoving -= PositionChange;
        playerModel.PlayerLooking -= DirectionChange;
        playerModel.PlayerShooting -= PlayerShootAnim;
        playerModel.PlayerJumping -= PlayerJump;
        playerModel.PlayerReloading -= PlayerReloadAnim;
        playerModel.AmmoChanging -= RefreshAmmoUI;
    }

    private void PositionChange(int speed, int axeleration, Vector3 direction)
    {
        playerView.SetPosition(speed, direction);

        if (axeleration != 1)
            playerView.StartRunAnim();
        else
            playerView.StopRunAnim();
    }

    private void DirectionChange(float mouseLookX, float verticalRotation)
    {
        playerView.SetRotation(mouseLookX, verticalRotation);
    }

    private void PlayerShootAnim(bool isShootDelay)
    {
        playerView.ShootAnim(isShootDelay);
    }

    private void PlayerJump(int jumpForce)
    {
        playerView.Jump(jumpForce);
    }

    private void PlayerReloadAnim()
    {
        playerView.Reload();
    }

    private void RefreshAmmoUI(int ammoCount, int ammoMagazineCount)
    {
        playerView.RefreshAmmoUI(ammoCount, ammoMagazineCount);
    }

    private void GroundDetector(bool isGrounded)
    {
        OnGroundedStateChange?.Invoke(isGrounded);
    }

    private void PlayerDeath()
    {
        Dispose();
    }
}
