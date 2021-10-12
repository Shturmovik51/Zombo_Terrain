using UnityEngine;

namespace ZomboTerrain
{
    public sealed class InputMouseY : IUserAxisInput
    {
        public float GetAxis()
        {
            return Input.GetAxis(AxisManager.MouseY);
        }
    }
}
