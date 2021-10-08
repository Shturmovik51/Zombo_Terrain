using System;
using UnityEngine;

namespace ZomboTerrain
{
    public sealed class InputVertical : IUserInput
    {
        public float GetAxis()
        {
            return Input.GetAxis(AxisManager.Vertical);
        }
    }
}
