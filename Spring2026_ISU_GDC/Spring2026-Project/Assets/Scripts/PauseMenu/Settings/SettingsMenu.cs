using UnityEngine;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject SettingsContainer;
    [SerializeField] GameObject MenuContainer;
    [SerializeField] Slider MasterVolSlider;
    [SerializeField] Slider MusicVolSlider;
    [SerializeField] Slider SFXVolSlider;
    [SerializeField] Button BlindButton;
    private bool Pressed=false;
    //private Button myButton;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ColorBlindButton()
    {
        //GameObject buttonGameObject = GameObject.Find("Color Blind Button");

        
        if (Pressed)
        {
            BlindButton.image.color = Color.white;
            Pressed = false;
        }
        else
        {
            BlindButton.image.color = Color.gray;
            Pressed = true;
        }
    }

    public void BackButton()
    {
        SettingsContainer.SetActive(false);
        MenuContainer.SetActive(true);
    }

    public void MasterVolumeSlider()
    {
        int vol = (int)MasterVolSlider.value;
        
        
    }

    public void EffectVolumeSlider()
    {
        int vol = (int)SFXVolSlider.value;
    }

    public void MusicVolumeSlider()
    {
        int vol = (int)MusicVolSlider.value;
    }
}
