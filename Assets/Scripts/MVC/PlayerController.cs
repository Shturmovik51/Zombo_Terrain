using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerView playerView;
    private PlayerModel playerModel;
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
    }

    public void Disable()
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
        //playerView.Jump(jumpForce);
    }

    private void PlayerReloadAnim()
    {
        playerView.Reload();
    }

    private void RefreshAmmoUI(int ammoCount, int ammoMagazineCount)
    {
        playerView.RefreshAmmoUI(ammoCount, ammoMagazineCount);
    }



}
