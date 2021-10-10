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
        public event Action OnClickReloadButton = delegate { };
        public event Action OnClickSaveGameButton;
        public event Action OnClickLoadGameButton;


        private readonly IUserInput _inputHorizontal;
        private readonly IUserInput _inputVertical;
        private readonly IUserInput _inputMouseX;
        private readonly IUserInput _inputMouseY;
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
            GetMoveInput();
            GetMouseInput();
            ShootButton();
            RunButton();
            JumpButton();
            ReloadButton();
            SaveGameButton();
            LoadGameButton();
           // OnClickJumpButton.Invoke(_inputKeys.GetKeyJump());
        }

        public void GetMoveInput()
        {            
             OnChangeMoveAxis.Invoke((_inputVertical.GetAxis(), _inputHorizontal.GetAxis()));
        }

        private void GetMouseInput()
        {
            OnChangeLookAxis.Invoke((_inputMouseX.GetAxis(), _inputMouseY.GetAxis()));
        }

        private void ShootButton()
        {
            if (_inputKeys.GetKeyShoot(_inputKeysData))
                OnClickShootButton.Invoke();
        }
        private void RunButton()
        {
            if (_inputKeys.GetKeyRunDown(_inputKeysData))
                OnClickRunButton.Invoke(true);
            if (_inputKeys.GetKeyRunUp(_inputKeysData))
                OnClickRunButton.Invoke(false);
        }
        private void JumpButton()
        {
            if (_inputKeys.GetKeyJump(_inputKeysData))
                OnClickJumpButton.Invoke();
        }
        private void ReloadButton()
        {
            if (_inputKeys.GetKeyReload(_inputKeysData))
                OnClickReloadButton.Invoke();
        }
        private void SaveGameButton()
        {
            if (_inputKeys.GetKeySave(_inputKeysData))
                OnClickSaveGameButton.Invoke();
        }
        private void LoadGameButton()
        {
            if (_inputKeys.GetKeyLoad(_inputKeysData))
                OnClickLoadGameButton.Invoke();
        }




    }
}
