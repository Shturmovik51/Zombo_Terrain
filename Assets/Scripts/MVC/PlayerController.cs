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
        playerModel.PositionChange += PositionChange;
        playerModel.PlayerShooting += PlayerShootAnim;

    }
    public void Disable()
    {
        playerModel.PositionChange -= PositionChange;
        playerModel.PlayerShooting -= PlayerShootAnim;
    }

    private void PositionChange(int speed, int axeleration, Vector3 direction)
    {
        playerView.SetPosition(speed, direction);

        if (axeleration != 1)
            playerView.StartRunAnim();        
        else
            playerView.StopRunAnim();
    }

    private void PlayerShootAnim(bool isShootDelay)
    {
        playerView.ShootAnim(isShootDelay);
    }


}
