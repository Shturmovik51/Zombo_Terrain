using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button DayBtn;
    [SerializeField] private Button SunSetBtn;
    [SerializeField] private Button NightBtn;
    [SerializeField] private Button SunRiseBtn;
    [SerializeField] private DailyCycle dailyCycle;   

    private void Awake()
    {
        DayBtn.onClick.AddListener(OnClickDayBtn);
        SunSetBtn.onClick.AddListener(OnClickSunSetBtn);
        NightBtn.onClick.AddListener(OnClickNightBtn);
        SunRiseBtn.onClick.AddListener(OnClickSunRiseBtn);
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
}
