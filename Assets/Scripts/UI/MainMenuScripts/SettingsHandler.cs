using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField] Image coldImage;
    [SerializeField] Image warmImage;
    [SerializeField] Image brightnessImage;
    [SerializeField] GameObject settingsPage;

    private void Start()
    {
        applyCold(SettingsData.coldValue);
        applyWarm(SettingsData.warmValue);
        applyBrightness(SettingsData.brightnessValue);
    }
    public void applyCold(float sliderValue)
    {
        Color newColor = coldImage.color;
        newColor.a = sliderValue;
        coldImage.color = newColor;
        SettingsData.coldValue = sliderValue;
    }
    public void applyWarm(float sliderValue)
    {
        Color newColor = warmImage.color; 
        newColor.a = sliderValue;         
        warmImage.color = newColor;
        SettingsData.warmValue = sliderValue;
    }
    public void applyBrightness(float sliderValue)
    {
        Color newColor = brightnessImage.color;
        newColor.a = sliderValue;         
        brightnessImage.color = newColor;
        SettingsData.brightnessValue = sliderValue;
    }
    public void HideSettingPage()
    {
        settingsPage.SetActive(false);
    }
    public void ShowSettingPage()
    {
        settingsPage.SetActive(true);
    }

}
