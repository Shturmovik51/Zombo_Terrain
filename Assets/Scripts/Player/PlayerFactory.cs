namespace ZomboTerrain
{
    public sealed class PlayerFactory
    {
        private readonly PlayerData _playerData;

        public PlayerFactory(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public PlayerModel CreatePlayerModel()
        {
            return new PlayerModel(_playerData.Health, _playerData.MoveSpeed, _playerData.JumpForce, /*_playerData.PlayerWeapon,*/
                                    _playerData.AmmoCount, _playerData.Axeleration);
        }
    }
}
