using System;
using UnityEngine;

public sealed class InputVertical : IUserInput
{
    public event Action<float> OnChangeAxis = delegate { };

    public void GetAxis()
    {
        OnChangeAxis.Invoke(Input.GetAxis(AxisManager.Vertical));
    }
}
