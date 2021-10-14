using UnityEngine;
using UnityEngine.UI;

namespace ZomboTerrain
{
    [System.Serializable]
    public struct UIButtons
    {
        [SerializeField] private Button _dayButton;
        [SerializeField] private Button _sunSetButton;
        [SerializeField] private Button _nightButton;
        [SerializeField] private Button _sunRiseButton;
        [SerializeField] private Button _restartGameButtonInPause;
        [SerializeField] private Button _restartGameButtonInEndScreen;
        [SerializeField] private Button _continueGameButtonInPause;
        [SerializeField] private Button _continueGameButtonInEndScreen;
        [SerializeField] private Button _nuarEffectButton;
        [SerializeField] private Button _fishEyeEffectButton;
        [SerializeField] private Button _eyeFocusEffectButton;
        [SerializeField] private Button _eyeAddictiveEffectButton;

        public Button DayButton => _dayButton;
        public Button SunSetButton => _sunSetButton;
        public Button NightButton => _nightButton;
        public Button SunRiseButton => _sunRiseButton;
        public Button RestartGameButtonInPause => _restartGameButtonInPause;
        public Button RestartGameButtonInEndScreen => _restartGameButtonInEndScreen;
        public Button ContinueGameButtonInPause => _continueGameButtonInPause;
        public Button ContinueGameButtonInEndScreen => _continueGameButtonInEndScreen;
        public Button NuarEffectButton => _nuarEffectButton;
        public Button FishEyeEffectButton => _fishEyeEffectButton;
        public Button EyeFocusEffectButton => _eyeFocusEffectButton;
        public Button EyeAddictiveEffectButton => _eyeAddictiveEffectButton;
    }
}

