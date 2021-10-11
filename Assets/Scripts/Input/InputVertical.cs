using System;
using UnityEngine;

namespace ZomboTerrain
{
    public sealed class InputVertical : IUserAxisInput
    {
        public float GetAxis()
        {
            return Input.GetAxis(AxisManager.Vertical);
        }
    }
}
