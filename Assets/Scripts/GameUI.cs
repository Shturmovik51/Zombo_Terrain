using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _endGameScreen;
    [SerializeField] private Button _dayButton;
    [SerializeField] private Button _sunSetButton;
    [SerializeField] private Button _nightButton;
    [SerializeField] private Button _sunRiseButton;
    [SerializeField] private Button _restartGameButtonInPause;
    [SerializeField] private Button _restartGameButtonInEndScreen;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private TextMeshProUGUI _ammoMagazineText;

    private Vector3 _daySunPosition = new Vector3(80, 0, 0);
    private Vector3 _nightSunPosition = new Vector3(-90, 0, 0);
    private Vector3 _sunSetSunPosition = new Vector3(-5, 0, 0);
    private Vector3 _sunRiseSunPosition = new Vector3(190, 0, 0);

    public TextMeshProUGUI AmmoText { get => _ammoText; set => _ammoText = value; }
    public TextMeshProUGUI AmmoMagazineText { get => _ammoMagazineText; set => _ammoMagazineText = value; }

    private void Awake()
    {
        _dayButton.onClick.AddListener(OnClickDayButton);
        _sunSetButton.onClick.AddListener(OnClickSunSetButton);
        _nightButton.onClick.AddListener(OnClickNightButton);
        _sunRiseButton.onClick.AddListener(OnClickSunRiseButton);
        _restartGameButtonInPause.onClick.AddListener(OnClickRestartGameButton);
        _restartGameButtonInEndScreen.onClick.AddListener(OnClickRestartGameButton);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pausePanel.activeInHierarchy)
                PausePanelOnOff(CursorLockMode.Locked, false, 1);          
            else
                PausePanelOnOff(CursorLockMode.None, true, 0);                      
        }
    }

    private void OnDestroy()
    {
        _dayButton.onClick.RemoveAllListeners();
        _sunSetButton.onClick.RemoveAllListeners();
        _nightButton.onClick.RemoveAllListeners();
        _sunRiseButton.onClick.RemoveAllListeners();
    }

    private void PausePanelOnOff(CursorLockMode cursorLockMode, bool isVisible, float timeScale)
    {
        _pausePanel.gameObject.SetActive(isVisible);
        ChangeGameTimeAndCorsor(cursorLockMode, isVisible, timeScale);
    }

    private void ChangeGameTimeAndCorsor(CursorLockMode cursorLockMode, bool isVisible, float timeScale)
    {        
        Cursor.visible = isVisible;
        Cursor.lockState = cursorLockMode;
        Time.timeScale = timeScale;
    }

    private void OnClickDayButton()
    {
        //_gameManager._dailyCycle.DailyCycleTimeJump(Quaternion.Euler(_daySunPosition));       
    }
    private void OnClickSunSetButton()
    {
        //_gameManager._dailyCycle.DailyCycleTimeJump(Quaternion.Euler(_sunSetSunPosition));       
    }
    private void OnClickNightButton()
    {
        //_gameManager._dailyCycle.DailyCycleTimeJump(Quaternion.Euler(_nightSunPosition));       
    }
    private void OnClickSunRiseButton()
    {
        //_gameManager._dailyCycle.DailyCycleTimeJump(Quaternion.Euler(_sunRiseSunPosition));       
    }   

    private void OnClickRestartGameButton()
    {
        SceneManager.LoadScene(0);
        ChangeGameTimeAndCorsor(CursorLockMode.Locked, false, 1);
    }

    public void StartEndGameScreen()
    {
        _endGameScreen.SetActive(true);
        ChangeGameTimeAndCorsor(CursorLockMode.None, true, 0);
    }
}
