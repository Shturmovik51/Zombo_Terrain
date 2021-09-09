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
        playerModel.PlayerMooving += PositionChange;
        playerModel.PlayerLooking += DirectionChange;
        playerModel.PlayerShooting += PlayerShootAnim;
        playerModel.PlayerJumping += PlayerJump;

    }
    public void Disable()
    {
        playerModel.PlayerMooving -= PositionChange;
        playerModel.PlayerLooking -= DirectionChange;
        playerModel.PlayerShooting -= PlayerShootAnim;
        playerModel.PlayerJumping -= PlayerJump;
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

    private void PlayerJump()
    {

    }



}
