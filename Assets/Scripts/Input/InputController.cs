using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class InputController : IUpdatable
{
    private readonly IUserInput _inputHorizontal;
    private readonly IUserInput _inputVertical;

    public InputController((IUserInput horizontalInput, IUserInput verticalInput) input)
    {
        _inputHorizontal = input.horizontalInput;
        _inputVertical = input.verticalInput;
    }

    public void LocalUpdate(float deltaTime)
    {
        _inputHorizontal.GetAxis();
        _inputVertical.GetAxis();
    }
}
