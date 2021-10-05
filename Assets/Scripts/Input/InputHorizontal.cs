using System;
using UnityEngine;

public sealed class InputHorizontal : IUserInput
{
    public event Action<float> OnChangeAxis;

    public void GetAxis()
    {
        OnChangeAxis.Invoke(Input.GetAxis(AxisManager.Horizontal));
    }
}
