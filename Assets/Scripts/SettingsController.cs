using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public static string UI_SCALE = "UI_SCALE";
    public Canvas uiCanvas;
    public float defaultUIScale = 1f;
    public float uiScaleValue = 1f;
    public Slider uiScaleSlider;

    public Button applyButton;

    private void Start()
    {
        ResetSettings();
    }

    private void Update() {
        if (uiScaleSlider != null && applyButton != null) {
            if (uiScaleSlider.value == uiScaleValue) {
                applyButton.interactable = false;
            } else if (!applyButton.interactable) {
                applyButton.interactable = true;
            }
        }
    }

    private void LoadSettingsData()
    {
        if (PlayerPrefs.HasKey(UI_SCALE))
        {
            uiScaleValue = PlayerPrefs.GetFloat(UI_SCALE);
            if (uiScaleSlider != null)
            {
                uiScaleSlider.value = uiScaleValue;
            }
        }
        else
        {
            PlayerPrefs.SetFloat(UI_SCALE, defaultUIScale);
        }
    }

    public void ResetSettings() {
        LoadSettingsData();
        ApplySettings();
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
            uiScaleValue = uiScaleSlider.value;
            PlayerPrefs.SetFloat(UI_SCALE, uiScaleValue);
            PlayerPrefs.Save();
        }
    }

    private void UpdateUIScale()
    {
        if (uiCanvas != null)
        {
            uiCanvas.scaleFactor = this.uiScaleValue;
        }
    }
}