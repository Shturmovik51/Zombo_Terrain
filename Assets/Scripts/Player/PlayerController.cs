using UnityEngine;

namespace ZomboTerrain
{
    public sealed class PlayerController : IInitialisible, IUpdatable, ICleanable, IController
    {
        private PlayerView _playerView;
        private PlayerModel _playerModel;
        private InputController _inputController;
        public PlayerController(PlayerView view, PlayerModel model, InputController inputController)
        {
            _playerView = view;
            _playerModel = model;
            _inputController = inputController;
        }

        public void Initialization()
        {
            _playerModel.PlayerWeapon.OnAmmoChange += AmmoCountChange;
            _playerModel.PlayerWeapon.OnWeaponShoot += WeaponRecoil;
            _playerModel.PlayerWeapon.OnEmptyAmmo += PLayerReloadWeapon;
            _inputController.OnChangeLookAxis += PlayerLook;
            _inputController.OnChangeMoveAxis += PlayerMove;
            _inputController.OnClickShootButton += PLayerShoot;
            _inputController.OnClickRunButton += Axeleration;
            _inputController.OnClickJumpButton += PlayerJump;
            _inputController.OnClickReloadButton += PLayerReloadWeapon;
        }

        public void CleanUp()
        {
            _playerModel.PlayerWeapon.OnAmmoChange -= AmmoCountChange;
            _playerModel.PlayerWeapon.OnWeaponShoot -= WeaponRecoil;
            _playerModel.PlayerWeapon.OnEmptyAmmo -= PLayerReloadWeapon;
        }

        private void Axeleration(bool value)
        {
            _playerModel.IsRun = value;
        }

        public void PlayerLook((float x, float y) input)
        {
            _playerModel.VerticalRotation -= input.y;
            _playerModel.VerticalRotation = Mathf.Clamp(_playerModel.VerticalRotation, -45f, 45f);

            _playerView.SetRotation(input.x, _playerModel.VerticalRotation);
        }

        public void PlayerMove((float x, float z) step)
        {
            var moveDirection = (_playerView.transform.forward * step.x + _playerView.transform.right * step.z);
            moveDirection *= _playerModel.MoveSpeed;

            if (_playerModel.IsRun)
            {
                moveDirection *= _playerModel.Axeleration;
                _playerView.StartRunAnim();
            }
            else
                _playerView.StopRunAnim();

            _playerView.SetPosition(moveDirection);
        }        

        public void PLayerShoot()
        {
            _playerModel.PlayerWeapon.Shoot(_playerModel.AmmoCount);
        }

        private void WeaponRecoil(bool isShootDelay)
        {
            _playerView.ShootAnim(isShootDelay);
        }

        public void PlayerJump()
        {
            if (!_playerView.IsGrounded())
                return;

            _playerView.Jump(_playerModel.JumpForce);
        }


        private void AmmoCountChange(int ammoCount, int ammoMagazineCount)
        {
            _playerModel.AmmoCount = ammoCount;

            RefreshAmmoCount();
            RefreshAmmoMagazineCount(ammoMagazineCount);
        }

        private void RefreshAmmoCount()
        {
            _playerView.RefreshAmmoCountUI(_playerModel.AmmoCount);
        }

        private void RefreshAmmoMagazineCount(int ammoMagazineCount)
        {
            _playerView.RefreshAmmoMagazineCountUI(ammoMagazineCount);
        }

        public void ChangeMoveSpeed(int value)
        {
            Debug.Log(value);
            _playerModel.MoveSpeed += value;
        }
        public void ChangeJumpForce(int value)
        {
            _playerModel.JumpForce += value;
        }

        public void PLayerReloadWeapon()
        {
            if (_playerModel.AmmoCount == 0)
                return;

            _playerModel.PlayerWeapon.ReloadWeapon(_playerModel.AmmoCount);
            _playerView.ReloadAnimation();
        } 

        private void RefreshHealthBar()
        {
            //soon in update
        }

        public void LocalUpdate(float deltatime)
        {

        }
    }
}
