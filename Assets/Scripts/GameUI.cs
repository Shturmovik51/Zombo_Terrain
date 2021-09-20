using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel; 
    [SerializeField] private Button _dayButton;
    [SerializeField] private Button _sunSetButton;
    [SerializeField] private Button _nightButton;
    [SerializeField] private Button _sunRiseButton;
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
        _dayButton.onClick.AddListener(OnClickDayBtn);
        _sunSetButton.onClick.AddListener(OnClickSunSetBtn);
        _nightButton.onClick.AddListener(OnClickNightBtn);
        _sunRiseButton.onClick.AddListener(OnClickSunRiseBtn);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pausePanel.activeInHierarchy)
                ChangeStateGame(CursorLockMode.Locked, false, 1);
            else
                ChangeStateGame(CursorLockMode.None, true, 0);
        }
    }

    private void OnDestroy()
    {
        _dayButton.onClick.RemoveAllListeners();
        _sunSetButton.onClick.RemoveAllListeners();
        _nightButton.onClick.RemoveAllListeners();
        _sunRiseButton.onClick.RemoveAllListeners();
    }

    private void ChangeStateGame(CursorLockMode cursorLockMode, bool isVisible, float timeScale)
    {
        _pausePanel.gameObject.SetActive(isVisible);
        Cursor.visible = isVisible;
        Cursor.lockState = cursorLockMode;
        Time.timeScale = timeScale;
    }
    private void OnClickDayBtn()
    {
        _gameManager.dailyCycle.DailyCycleTimeJump(Quaternion.Euler(_daySunPosition));       
    }
    private void OnClickSunSetBtn()
    {
        _gameManager.dailyCycle.DailyCycleTimeJump(Quaternion.Euler(_sunSetSunPosition));       
    }
    private void OnClickNightBtn()
    {
        _gameManager.dailyCycle.DailyCycleTimeJump(Quaternion.Euler(_nightSunPosition));       
    }
    private void OnClickSunRiseBtn()
    {
        _gameManager.dailyCycle.DailyCycleTimeJump(Quaternion.Euler(_sunRiseSunPosition));       
    }   
}
