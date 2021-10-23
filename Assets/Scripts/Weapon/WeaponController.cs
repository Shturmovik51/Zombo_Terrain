namespace ZomboTerrain
{
    public class WeaponController : IInitialisible, IController
    {
        private PlayerModel _playerModel;
        private Weapon _weapon;
        public Weapon Weapon => _weapon;

        public WeaponController(PlayerModel playerModel, Weapon weapon)
        {
            _playerModel = playerModel;
            _weapon = weapon;
        }

        public void Initialization()
        {
            _playerModel.PlayerWeapon = _weapon;
            _weapon.RefreshAmmoMagazineText();
        }
    }
}
