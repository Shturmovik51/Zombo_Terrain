using UnityEngine;

namespace ZomboTerrain
{
    public sealed class InputHorizontal : IUserInput
    {
        public float GetAxis()
        {
           return Input.GetAxis(AxisManager.Horizontal);
        }
    }
}