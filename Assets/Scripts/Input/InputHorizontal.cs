using UnityEngine;

namespace ZomboTerrain
{
    public sealed class InputHorizontal : IUserAxisInput
    {
        public float GetAxis()
        {
           return Input.GetAxis(AxisManager.Horizontal);
        }
    }
}