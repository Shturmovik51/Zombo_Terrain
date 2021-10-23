using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ZomboTerrain
{
    public sealed class EndScreenController : IInitialisible, ICleanable, IController
    {
        public event Action<int> OnChangeKillsCount;

        private int _killsCountToWin;
        private GameObject _endScreen;
        private Button _restartGameButtonInEndScreen;
        private Button _continueGameButtonInEndScreen;

        public EndScreenController(UIButtons uIButtons, UIObjects uIObjects, int killsCountToWin)
        {
            _killsCountToWin = killsCountToWin;
            _endScreen = uIObjects.EndGameScreen;
            _restartGameButtonInEndScreen = uIButtons.RestartGameButtonInEndScreen;
            _continueGameButtonInEndScreen = uIButtons.ContinueGameButtonInEndScreen;
        }

        public void Initialization()
        {
            _restartGameButtonInEndScreen.onClick.AddListener(OnClickRestartGameButton);
            _continueGameButtonInEndScreen.onClick.AddListener(OnClickContinueGameButton);
            OnChangeKillsCount?.Invoke(_killsCountToWin);
        }

        public void CleanUp()
        {
            _restartGameButtonInEndScreen.onClick.RemoveListener(OnClickRestartGameButton);
            _continueGameButtonInEndScreen.onClick.RemoveListener(OnClickContinueGameButton);
        }

        public void KillsCountDown()
        {
            _killsCountToWin--;
            OnChangeKillsCount?.Invoke(_killsCountToWin);

            if (_killsCountToWin == 0)
            {
                _endScreen.gameObject.SetActive(true);
                ChangeGameTimeAndCursor(CursorLockMode.None, true, 0);
            }
        }

        private void OnClickRestartGameButton()
        {
            SceneManager.LoadScene(0);
            ChangeGameTimeAndCursor(CursorLockMode.Locked, false, 1);
        }
        private void OnClickContinueGameButton()
        {
            _endScreen.gameObject.SetActive(false);
            ChangeGameTimeAndCursor(CursorLockMode.Locked, false, 1);
        }

        private void ChangeGameTimeAndCursor(CursorLockMode cursorLockMode, bool isVisible, float timeScale)
        {
            Cursor.visible = isVisible;
            Cursor.lockState = cursorLockMode;
            Time.timeScale = timeScale;
        }

    }
}
