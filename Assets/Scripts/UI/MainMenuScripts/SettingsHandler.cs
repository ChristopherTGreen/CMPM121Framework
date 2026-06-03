using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField] Image coldImage;
    [SerializeField] Image warmImage;
    [SerializeField] Image brightnessImage;
    [SerializeField] GameObject settingsPage;
    public void applyCold(float sliderValue)
    {
        Color newColor = coldImage.color;
        newColor.a = sliderValue;
        coldImage.color = newColor;
    }
    public void applyWarm(float sliderValue)
    {
        Color newColor = warmImage.color; 
        newColor.a = sliderValue;         
        warmImage.color = newColor;       
    }
    public void applyBrightness(float sliderValue)
    {
        Color newColor = brightnessImage.color;
        newColor.a = sliderValue;         
        brightnessImage.color = newColor;       
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
