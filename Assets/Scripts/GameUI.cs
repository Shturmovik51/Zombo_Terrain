using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private DailyCycle dailyCycle;   
    [SerializeField] private Button DayBtn;
    [SerializeField] private Button SunSetBtn;
    [SerializeField] private Button NightBtn;
    [SerializeField] private Button SunRiseBtn;
    [SerializeField] private Button controlWSADBtn;
    [SerializeField] private Button control8546Btn;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI ammoMagazineText;

    public TextMeshProUGUI AmmoText { get => ammoText; set => ammoText = value; }
    public TextMeshProUGUI AmmoMagazineText { get => ammoMagazineText; set => ammoMagazineText = value; }

    private void Awake()
    {
        DayBtn.onClick.AddListener(OnClickDayBtn);
        SunSetBtn.onClick.AddListener(OnClickSunSetBtn);
        NightBtn.onClick.AddListener(OnClickNightBtn);
        SunRiseBtn.onClick.AddListener(OnClickSunRiseBtn);
        controlWSADBtn.onClick.AddListener(OnClickControlWSADBtn);
        control8546Btn.onClick.AddListener(OnClickControl8546Btn);
    }

    private void Start()
    {
        OnClickControlWSADBtn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeInHierarchy)
                ChangeStateGame(CursorLockMode.Locked, false, 1);
            else
                ChangeStateGame(CursorLockMode.None, true, 0);
        }
    }

    private void OnDestroy()
    {
        DayBtn.onClick.RemoveAllListeners();
        SunSetBtn.onClick.RemoveAllListeners();
        NightBtn.onClick.RemoveAllListeners();
        SunRiseBtn.onClick.RemoveAllListeners();
    }

    private void ChangeStateGame(CursorLockMode cursorLockMode, bool isVisible, float timeScale)
    {
        pausePanel.gameObject.SetActive(isVisible);
        Cursor.visible = isVisible;
        Cursor.lockState = cursorLockMode;
        Time.timeScale = timeScale;
    }
    private void OnClickDayBtn()
    {
        dailyCycle.DailyCycleTimeJump(Quaternion.Euler(new Vector3(80, 0, 0)));       
    }
    private void OnClickSunSetBtn()
    {
        dailyCycle.DailyCycleTimeJump(Quaternion.Euler(new Vector3(-5, 0, 0)));       
    }
    private void OnClickNightBtn()
    {
        dailyCycle.DailyCycleTimeJump(Quaternion.Euler(new Vector3(-90, 0, 0)));       
    }
    private void OnClickSunRiseBtn()
    {
        dailyCycle.DailyCycleTimeJump(Quaternion.Euler(new Vector3(190, 0, 0)));       
    }
    private void OnClickControlWSADBtn()
    {
        Player.instance.SetWSADcontrol();
        controlWSADBtn.image.color = Color.green;
        control8546Btn.image.color = Color.white;
    }
    private void OnClickControl8546Btn()
    {
        Player.instance.Set8546control();
        controlWSADBtn.image.color = Color.white;
        control8546Btn.image.color = Color.green;
    }
}
