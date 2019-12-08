using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public static string UI_SCALE = "UI_SCALE";
    public Canvas uiCanvas;
    public float defaultUIScale = 1f;
    public float uiScale = 1f;
    public Slider uiScaleSlider;

    private void Start()
    {
        LoadSettingsData();
        ApplySettings();
    }

    private void LoadSettingsData()
    {
        if (PlayerPrefs.HasKey(UI_SCALE))
        {
            uiScale = PlayerPrefs.GetFloat(UI_SCALE);
            if (uiScaleSlider != null)
            {
                uiScaleSlider.value = uiScale;
            }
        }
        else
        {
            PlayerPrefs.SetFloat(UI_SCALE, defaultUIScale);
        }
    }

    public void ApplySettings()
    {
        ApplyUIScale();
        UpdateUIScale();
    }

    public void ApplyUIScale()
    {
        if (uiScaleSlider != null)
        {
            uiScale = uiScaleSlider.value;
            PlayerPrefs.SetFloat(UI_SCALE, uiScale);
            PlayerPrefs.Save();
        }
    }

    private void UpdateUIScale()
    {
        if (uiCanvas != null)
        {
            uiCanvas.scaleFactor = this.uiScale;
        }
    }
}