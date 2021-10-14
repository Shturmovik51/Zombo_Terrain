using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZomboTerrain
{
    [CreateAssetMenu(fileName = "UIElementsData", menuName = "Database/UIElementsData")]
    public class UserInterfaceElementsData : ScriptableObject
    {
        [SerializeField] private Button _dayButton;
        [SerializeField] private Button _sunSetButton;
        [SerializeField] private Button _nightButton;
        [SerializeField] private Button _sunRiseButton;
        [SerializeField] private Button _restartGameButtonInPause;
        [SerializeField] private Button _restartGameButtonInEndScreen;
        [SerializeField] private Button _nuarEffectButton;
        [SerializeField] private Button _fishEyeEffectButton;
        [SerializeField] private Button _eyeFocusEffectButton;
        [SerializeField] private Button _eyeAddictiveEffectButton;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _endGameScreen;
        [SerializeField] private TextMeshProUGUI _ammoText;
        [SerializeField] private TextMeshProUGUI _ammoMagazineText;
        [SerializeField] private TextMeshProUGUI _timeText;

        public Button DayButton => _dayButton;
        public Button SunSetButton => _sunSetButton;
        public Button NightButton => _nightButton;
        public Button SunRiseButton => _sunRiseButton;
        public Button RestartGameButtonInPause => _restartGameButtonInPause;
        public Button RestartGameButtonInEndScreen => _restartGameButtonInEndScreen;
        public Button NuarEffectButton => _nuarEffectButton;
        public Button FishEyeEffectButton => _fishEyeEffectButton;
        public Button EyeFocusEffectButton => _eyeFocusEffectButton;
        public Button EyeAddictiveEffectButton => _eyeAddictiveEffectButton;
        public GameObject PausePanel => _pausePanel;
        public GameObject EndGameScreen => _endGameScreen;
        public TextMeshProUGUI AmmoText => _ammoText;
        public TextMeshProUGUI AmmoMagazineText => _ammoMagazineText;
        public TextMeshProUGUI TimeText => _timeText;
    }
}
