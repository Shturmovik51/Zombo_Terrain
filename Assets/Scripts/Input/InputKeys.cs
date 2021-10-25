using System;
using UnityEngine;

namespace ZomboTerrain
{
    public sealed class InputKeys
    {
        public void GetKeyShoot(InputKeysData _inputKeysData, Action action)
        {
            if (Input.GetKey(_inputKeysData.Shoot)) action.Invoke();
        }

        public void GetKeyRunDown(InputKeysData _inputKeysData, Action<bool> action)
        {
            if (Input.GetKeyDown(_inputKeysData.Run)) action.Invoke(true);
        }

        public void GetKeyRunUp(InputKeysData _inputKeysData, Action<bool> action)
        {
            if (Input.GetKeyUp(_inputKeysData.Run)) action.Invoke(false);
        }

        public void GetKeyJump(InputKeysData _inputKeysData, Action action)
        {
            if (Input.GetKey(_inputKeysData.Jump)) action.Invoke();
        }

        public void GetKeyReloadDown(InputKeysData _inputKeysData, Action action)
        {
            if (Input.GetKeyDown(_inputKeysData.Reload)) action?.Invoke();
        }

        public void GetKeySaveGameDown(InputKeysData _inputKeysData, Action action)
        {
            if (Input.GetKeyDown(_inputKeysData.SaveGame)) action?.Invoke();
        }

        public void GetKeyLoadGameDown(InputKeysData _inputKeysData, Action action)
        {
            if (Input.GetKeyDown(_inputKeysData.LoadGame)) action?.Invoke();
        }

        public void GetKeyFlashLightDown(InputKeysData _inputKeysData, Action action)
        {
            if (Input.GetKeyDown(_inputKeysData.FlashLight)) action?.Invoke();
        }

        public void GetKeyPauseDown(InputKeysData _inputKeysData, Action action)
        {
            if (Input.GetKeyDown(_inputKeysData.Pause)) action?.Invoke();
        }
    }
}
