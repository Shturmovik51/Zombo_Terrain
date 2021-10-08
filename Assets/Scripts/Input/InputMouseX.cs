using UnityEngine;

namespace ZomboTerrain
{
    public sealed class InputMouseX : IUserInput
    {
        public float GetAxis()
        {
            return Input.GetAxis(AxisManager.MouseX);
        }
    }
}
