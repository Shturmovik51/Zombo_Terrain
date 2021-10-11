using UnityEngine;

namespace ZomboTerrain
{
    public sealed class InputMouseX : IUserAxisInput
    {
        public float GetAxis()
        {
            return Input.GetAxis(AxisManager.MouseX);
        }
    }
}
