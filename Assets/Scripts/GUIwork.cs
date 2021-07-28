using UnityEngine;
using UnityEngine.UI;

public class GUIwork : MonoBehaviour
{
    [SerializeField] private Texture2D health;
    [SerializeField] private Texture2D healthBar;
    [SerializeField] private Color avatarColor;
    [SerializeField] private Color avatarBGColor;
    [SerializeField] private Image avatar;
    [SerializeField] private Image avatarBG;   
    
    private float deltaHealthLength;
    private float healthLengthMax;
    private float healthLength;
    private int currentHealth;

    private void Start()
    {
        healthLengthMax = 200;

        if (Player.instance != null)
            currentHealth = Player.instance.PlayerHealth.CurrentHealth;

        deltaHealthLength = healthLengthMax / currentHealth;
    }

    void OnGUI()
    {
        if (Time.timeScale == 0)
        {
            avatarColor = avatar.color;

            GUI.Label(new Rect(50, 50, 200, 20), "Avatar Color");
            avatarColor = RGBSlider(new Rect(50, 70, 200, 20), avatarColor);
            GUI.Label(new Rect(50, 150, 200, 20), "Avatar BackGround Color");
            avatarBGColor = RGBSlider(new Rect(50, 170, 200, 20), avatarBGColor);

            avatar.color = avatarColor;
            avatarBG.color = avatarBGColor;
        }

        GUI.DrawTexture(new Rect(Screen.width * 0.5f - 100, 10, healthLengthMax, 20), healthBar);

        if (Player.instance != null)
            currentHealth = Player.instance.PlayerHealth.CurrentHealth;

        if (healthLength >= 0)
            healthLength = currentHealth * deltaHealthLength;
        else
            healthLength = 0;

        GUI.DrawTexture(new Rect(Screen.width * 0.5f - 100, 10, healthLength, 20), health);

    }

    Color RGBSlider(Rect screenRect, Color rgba)
    {
        rgba.r = LabelSlider(screenRect, rgba.r, 0f, 1f, "Red");
        screenRect.y += 15;
        rgba.g = LabelSlider(screenRect, rgba.g, 0f, 1f, "Green");
        screenRect.y += 15;
        rgba.b = LabelSlider(screenRect, rgba.b, 0f, 1f, "Blue");
        screenRect.y += 15;
        rgba.a = LabelSlider(screenRect, rgba.a, 0f, 1f, "Alpha");
        return rgba;
    }

    float LabelSlider(Rect screenRect, float sliderValue, float sliderMinVal, float sliderMaxValue, string labelText)
    {
        GUI.Label(screenRect, labelText);
        screenRect.x += screenRect.width * 0.5f;
        sliderValue = GUI.HorizontalSlider(screenRect, sliderValue, sliderMinVal, sliderMaxValue);
        return sliderValue;
    }
}
