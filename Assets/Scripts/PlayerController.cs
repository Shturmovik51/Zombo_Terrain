using System.Collections;
using UnityEngine;

public class PlayerController
{
    private PlayerView _playerView;
    private PlayerModel _playerModel;
   // private BuffTimerController _buffTimerController;

    public PlayerController(PlayerView view, PlayerModel model /*BuffTimerController buffTimerController*/)
    {
        _playerView = view;
        _playerModel = model;
       // _buffTimerController = buffTimerController;
    }

    public void Enable()
    {
        _playerModel.PlayerWeapon.OnAmmoChange += AmmoCountChange;
        _playerModel.PlayerWeapon.OnWeaponShoot += WeaponRecoil;
        _playerModel.PlayerWeapon.OnEmptyAmmo += PLayerReloadWeapon;
        _playerView.OnRecieveBuff += AddBuff;
        //_buffTimerController.BuffTimerView.OnRemoveBuff += RemoveBuff;
    }

    public void Disable()
    {
        _playerModel.PlayerWeapon.OnAmmoChange -= AmmoCountChange;
        _playerModel.PlayerWeapon.OnWeaponShoot -= WeaponRecoil;
        _playerModel.PlayerWeapon.OnEmptyAmmo -= PLayerReloadWeapon;
    }
    //float xMoveDir, float zMoveDir, Vector3 xMovement, Vector3 zMovement, bool isRun
    public void PlayerMove((float x,float z)step, (Vector3 x, Vector3 z)direction, bool isRun)
    {
        var moveDirection = (direction.x * step.x + direction.z * step.z);
        moveDirection *= _playerModel.MoveSpeed;

        if (isRun)
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

    public void PlayerLook(float mouseLookX, float mouseLookY)
    {
        _playerModel.VerticalRotation -= mouseLookY;
        _playerModel.VerticalRotation = Mathf.Clamp(_playerModel.VerticalRotation, -45f, 45f);

        _playerView.SetRotation(mouseLookX, _playerModel.VerticalRotation);
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

    public void PLayerReloadWeapon()
    {
        if (_playerModel.AmmoCount == 0)
            return;

        _playerModel.PlayerWeapon.ReloadWeapon(_playerModel.AmmoCount);
        _playerView.ReloadAnimation();
    }

    private void AddBuff(Buff buff)
    {
        if (buff.Type == BuffType.Ammo)
        {
            _playerModel.AmmoCount += buff.BonusValue;
            RefreshAmmoCount();
        }
        if (buff.Type == BuffType.Health)
        {
            _playerModel.Health += buff.BonusValue;
            RefreshHealthBar();
        }
        if (buff.Type == BuffType.Speed)
        {
            _playerModel.MoveSpeed += buff.BonusValue;
            _playerView.StartCoroutine(BuffTimeDuration(buff));
            buff.Duration = 0;
        }
        if (buff.Type == BuffType.Jump)
        {
            _playerModel.JumpForce += buff.BonusValue;
            _playerView.StartCoroutine(BuffTimeDuration(buff));
        }
    }

    public void RemoveBuff(Buff buff)
    {
        if (buff.Type == BuffType.Speed)
        {
            _playerModel.MoveSpeed -= buff.BonusValue;
        }
        if (buff.Type == BuffType.Jump)
        {
            _playerModel.JumpForce -= buff.BonusValue;
        }
    }

    private void PlayerDeath()
    {
        Disable();
    }

    private void RefreshHealthBar()
    {
        //soon in update
    }

    private IEnumerator BuffTimeDuration(Buff buff)
    {
        yield return new WaitForSeconds(buff.Duration);
        RemoveBuff(buff);
    }
}
