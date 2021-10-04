using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDestination
{
    private PlayerController _playerController;
    private Dictionary<BuffType, BuffMethods> _buffMethodContainer;
    public Dictionary<BuffType, BuffMethods> BuffMethodContainer => _buffMethodContainer;

    public BuffDestination(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void Enable()
    {
        _buffMethodContainer = new Dictionary<BuffType, BuffMethods>            //Designed By Aleksey Skvortsov
        {
            [BuffType.Speed] = _playerController.ChangeMoveSpeed,
            [BuffType.Jump] = _playerController.ChangeJumpForce
        };
    }
}
