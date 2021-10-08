using UnityEngine;

namespace ZomboTerrain
{
    public sealed class InputMouseY : IUserInput
    {
        public float GetAxis()
        {
            return Input.GetAxis(AxisManager.MouseY);
        }
    }
}
