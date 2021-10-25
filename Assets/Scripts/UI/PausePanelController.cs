using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ZomboTerrain
{
    public sealed class PausePanelController : IInitialisible, IController, ICleanable
    {
        private GameObject _pausePanel;
        private Button _dayButton;
        private Button _sunSetButton;
        private Button _nightButton;
        private Button _sunRiseButton;
        private Button _nuarEffectButton;
        private Button _fishEyeEffectButton;
        private Button _eyeFocusEffectButton;
        private Button _eyeAddictiveEffectButton;
        private Button _restartGameButtonInPause;
        private Button _continueGameButtonInPause;
        private InputController _inputController;
        private DailyCycleController _dailyCycleController;
        public Button NuarEffectButton => _nuarEffectButton;
        public Button FishEyeEffectButton => _fishEyeEffectButton;
        public Button EyeFocusEffectButton => _eyeFocusEffectButton;
        public Button EyeAddictiveEffectButton => _eyeAddictiveEffectButton;

        public PausePanelController(UIButtons uIButtons, UIObjects uIObjects, InputController inputController, 
                                        DailyCycleController dailyCycleController)
        {
            _dayButton = uIButtons.DayButton;
            _pausePanel = uIObjects.PausePanel;
            _nightButton = uIButtons.NightButton;
            _sunSetButton = uIButtons.SunSetButton;
            _sunRiseButton = uIButtons.SunRiseButton;
            _inputController = inputController;
            _nuarEffectButton = uIButtons.NuarEffectButton;
            _fishEyeEffectButton = uIButtons.FishEyeEffectButton;
            _dailyCycleController = dailyCycleController;
            _eyeFocusEffectButton = uIButtons.EyeFocusEffectButton;
            _restartGameButtonInPause = uIButtons.RestartGameButtonInPause;
            _eyeAddictiveEffectButton = uIButtons.EyeAddictiveEffectButton;
            _continueGameButtonInPause = uIButtons.ContinueGameButtonInPause;
        }

        public void Initialization()
        {            
            _inputController.OnClickPauseButton += PausePanelOnOff;
            _restartGameButtonInPause.onClick.AddListener(OnClickRestartGameButton);
            _continueGameButtonInPause.onClick.AddListener(OnClickContinueGameButton);
            _dayButton.onClick.AddListener(() => _dailyCycleController.SetDayTime(_dayButton));
            _nightButton.onClick.AddListener(() => _dailyCycleController.SetNightTime(_nightButton));
            _sunSetButton.onClick.AddListener(() => _dailyCycleController.SetSunSetTime(_sunSetButton));
            _sunRiseButton.onClick.AddListener(() => _dailyCycleController.SetSunRiseTime(_sunRiseButton));
        }

        public void CleanUp()
        {
            _inputController.OnClickPauseButton -= PausePanelOnOff;
            _restartGameButtonInPause.onClick.RemoveAllListeners();
            _continueGameButtonInPause.onClick.RemoveAllListeners();
            _dayButton.onClick.RemoveAllListeners();
            _sunSetButton.onClick.RemoveAllListeners();
            _nightButton.onClick.RemoveAllListeners();
            _sunRiseButton.onClick.RemoveAllListeners();
        }

        private void PausePanelOnOff()
        {
            var isActive = _pausePanel.gameObject.activeInHierarchy;

            if (!isActive)    
                ChangeGameTimeAndCursor(CursorLockMode.None, true, 0);
            else  
                ChangeGameTimeAndCursor(CursorLockMode.Locked, false, 1);

            _pausePanel.gameObject.SetActive(!isActive);
        }

        private void ChangeGameTimeAndCursor(CursorLockMode cursorLockMode, bool isVisible, float timeScale)
        {
            Cursor.visible = isVisible;
            Cursor.lockState = cursorLockMode;
            Time.timeScale = timeScale;
        }

        private void OnClickRestartGameButton()
        {
            SceneManager.LoadScene(0);
            ChangeGameTimeAndCursor(CursorLockMode.Locked, false, 1);
        }
        private void OnClickContinueGameButton()
        {
            _pausePanel.gameObject.SetActive(false);
            ChangeGameTimeAndCursor(CursorLockMode.Locked, false, 1);
        }

    }
}
