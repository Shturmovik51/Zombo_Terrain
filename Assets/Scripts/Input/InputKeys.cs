using UnityEngine;

namespace ZomboTerrain
{
    public class InputKeys: IUserInput
    {
        public bool GetKeyShoot(InputKeysData _inputKeysData)
        {
            return Input.GetKey(_inputKeysData.Shoot);
        }

        public bool GetKeyRunDown(InputKeysData _inputKeysData)
        {
            return Input.GetKeyDown(_inputKeysData.Run);
        }

        public bool GetKeyRunUp(InputKeysData _inputKeysData)
        {
            return Input.GetKeyUp(_inputKeysData.Run);
        }

        public bool GetKeyJump(InputKeysData _inputKeysData)
        {
            return Input.GetKey(_inputKeysData.Jump);
        }

        public bool GetKeyReload(InputKeysData _inputKeysData)
        {
            return Input.GetKey(_inputKeysData.Reload);
        }

        public float GetAxis()
        {
            throw new System.NotImplementedException();
        }
    }
}
