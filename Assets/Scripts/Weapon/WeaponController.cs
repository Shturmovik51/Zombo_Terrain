using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZomboTerrain
{
    public class WeaponController : IInitialisible, IController
    {
        PlayerModel _playerModel;
        Weapon _weapon;

        public WeaponController(PlayerModel playerModel, Weapon weapon)
        {
            _playerModel = playerModel;
            _weapon = weapon;
        }

        public void Initialization()
        {
            _playerModel.PlayerWeapon = _weapon;
        }
    }
}
