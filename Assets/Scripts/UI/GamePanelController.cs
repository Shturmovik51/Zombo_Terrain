using TMPro;

namespace ZomboTerrain
{
    public class GamePanelController : IInitialisible, ICleanable, IController
    {
        private TextMeshProUGUI _ammoText;
        private TextMeshProUGUI _timeText;
        private TextMeshProUGUI _ammoMagazineText;
        private TextMeshProUGUI _killsText;
        private EndScreenController _endScreenController;
        private PlayerView _playerView;
        private Weapon _weapon;
        public GamePanelController(UIFields uIFields, PlayerView playerView, WeaponController weaponController,
                                    EndScreenController endScreenController)
        {
            _ammoText = uIFields.AmmoText;
            _timeText = uIFields.TimeText;
            _killsText = uIFields.KillsText;
            _ammoMagazineText = uIFields.AmmoMagazineText;
            _playerView = playerView;
            _weapon = weaponController.Weapon;
            _endScreenController = endScreenController;
        }

        public void Initialization()
        {
            _playerView.OnChangeAmmo += ChangeAmmoText;
            _weapon.OnAmmoMagazineChange += ChangeAmmoMagazineText;
            _endScreenController.OnChangeKillsCount += ChangeKillsCountText;
        }

        public void CleanUp()
        {
            _playerView.OnChangeAmmo -= ChangeAmmoText;
            _weapon.OnAmmoMagazineChange -= ChangeAmmoMagazineText;
            _endScreenController.OnChangeKillsCount -= ChangeKillsCountText;
        }

        public void ChangeAmmoText(int value)
        {
            _ammoText.text = value.ToString();
        }

        public void ChangeAmmoMagazineText(int value)
        {
            _ammoMagazineText.text = value.ToString();
        }

        public void ChangeTimeText(string timeText)
        {
            _timeText.text = timeText;
        }

        public void ChangeKillsCountText(int value)
        {
            _killsText.text = value.ToString();
        }

    }
}
