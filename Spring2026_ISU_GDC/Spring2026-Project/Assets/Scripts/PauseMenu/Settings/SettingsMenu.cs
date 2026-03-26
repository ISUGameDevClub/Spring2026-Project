using System;
using Nomad.Core.Engine.Globals;
using Nomad.Core.Engine.Services;
using Nomad.Core.Engine.Windowing;
using Nomad.Core.ServiceRegistry.Globals;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject SettingsContainer;
    [SerializeField] GameObject MenuContainer;
    [SerializeField] Slider MasterVolSlider;
    [SerializeField] Slider MusicVolSlider;
    [SerializeField] Slider SFXVolSlider;

    [SerializeField] Dropdown windowModeList;
    [SerializeField] Toggle vsyncToggle;

    private bool Pressed=false;
    //private Button myButton;
    // Update is called once per frame
    void Update()
    {
        windowModeList.onValueChanged.AddListener(OnWindowModeChanged);
        vsyncToggle.onValueChanged.AddListener(OnVSyncModeChanged);
    }

	private void OnVSyncModeChanged(bool value)
    {
        QualitySettings.vSyncCount = value ? 1 : 0;
    }

	private void OnWindowModeChanged(int value)
    {
        var windowService = ServiceLocator.GetService<IWindowService>();
        switch (value)
        {
            case 0: // windowed
                windowService.Mode = WindowMode.Windowed;
                break;
            case 1: // borderless windowed
                windowService.Mode = WindowMode.BorderlessWindowed;
                break;
            case 2: // fullscreen, technically exclusive fullscreen
                windowService.Mode = WindowMode.ExclusiveFullscreen;
                break;
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
