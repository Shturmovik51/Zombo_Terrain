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
        public event Action OnClickPauseButton;


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
            _inputKeys.GetKeyPauseDown(_inputKeysData, OnClickPauseButton);

            if (Time.timeScale == Mathf.Round(0)) return;

            OnChangeMoveAxis.Invoke((_inputVertical.GetAxis(), _inputHorizontal.GetAxis()));
            OnChangeLookAxis.Invoke((_inputMouseX.GetAxis(), _inputMouseY.GetAxis()));
            _inputKeys.GetKeyShoot(_inputKeysData, OnClickShootButton);
            _inputKeys.GetKeyRunDown(_inputKeysData, OnClickRunButton);
            _inputKeys.GetKeyRunUp(_inputKeysData, OnClickRunButton);
            _inputKeys.GetKeyJump(_inputKeysData, OnClickJumpButton);
            _inputKeys.GetKeyReloadDown(_inputKeysData, OnClickReloadButton);
            _inputKeys.GetKeySaveGameDown(_inputKeysData, OnClickSaveGameButton);
            _inputKeys.GetKeyLoadGameDown(_inputKeysData, OnClickLoadGameButton);
            _inputKeys.GetKeyFlashLightDown(_inputKeysData, OnClickFlashLightButton);
        }        
    }
}
