using System;
using UnityEngine;

namespace ZomboTerrain
{
    public sealed class InputController : IUpdatable, IController
    {
        public event Action<(float, float)> OnChangeMoveAxis = delegate { };
        public event Action<(float, float)> OnChangeLookAxis = delegate { };
        public event Action OnClickShootButton = delegate { };
        public event Action<bool> OnClickRunButton = delegate { };
        public event Action OnClickJumpButton = delegate { };
        public event Action OnClickReloadButton;
        public event Action OnClickSaveGameButton;
        public event Action OnClickLoadGameButton;
        public event Action OnClickFlashLightButton;


        private readonly IUserAxisInput _inputHorizontal;
        private readonly IUserAxisInput _inputVertical;
        private readonly IUserAxisInput _inputMouseX;
        private readonly IUserAxisInput _inputMouseY;
        private readonly InputKeys _inputKeys;
        private InputKeysData _inputKeysData;

        public InputController(Data data)
        {
            _inputHorizontal = new InputHorizontal();
            _inputVertical = new InputVertical();
            _inputMouseX = new InputMouseX();
            _inputMouseY = new InputMouseY();
            _inputKeys = new InputKeys();

            _inputKeysData = data.InputKeysData;
        }

        public void LocalUpdate(float deltaTime)
        {
            OnChangeMoveAxis.Invoke((_inputVertical.GetAxis(), _inputHorizontal.GetAxis()));
            OnChangeLookAxis.Invoke((_inputMouseX.GetAxis(), _inputMouseY.GetAxis()));
            _inputKeys.GetKeyShoot(_inputKeysData, OnClickShootButton);
            _inputKeys.GetKeyRunDown(_inputKeysData, OnClickRunButton);
            _inputKeys.GetKeyRunUp(_inputKeysData, OnClickRunButton);
            _inputKeys.GetKeyJump(_inputKeysData, OnClickJumpButton);
            _inputKeys.GetKeyReload(_inputKeysData, OnClickReloadButton);
            _inputKeys.GetKeySaveGame(_inputKeysData, OnClickSaveGameButton);
            _inputKeys.GetKeyLoadGame(_inputKeysData, OnClickLoadGameButton);
            _inputKeys.GetKeyFlashLight(_inputKeysData, OnClickFlashLightButton);
        }        
    }
}
