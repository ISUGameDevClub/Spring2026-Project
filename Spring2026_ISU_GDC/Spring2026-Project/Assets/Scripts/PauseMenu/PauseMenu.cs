using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject MenuContainer;
    [SerializeField] GameObject SettingsContainer;
    [SerializeField] GameObject ControlContainer;
   
    public event Action OnPauseMenuClosed;

    private void Start()
    {
        OpenMenu();
    }

    public void OpenMenu()
    {
        Time.timeScale = 0.0f;
        MenuContainer.SetActive(true);
    }

    public void CloseMenu()
    {
        Time.timeScale = 1.0f;
        MenuContainer.SetActive(false);
        OnPauseMenuClosed?.Invoke();
        Destroy(gameObject);
    }
    
    public void ResumeButton()
    {
        CloseMenu();
    }

    public void SettingButton()
    {
        MenuContainer.SetActive(false);
        SettingsContainer.SetActive(true);
    }

    public void ControlButton()
    {
        MenuContainer.SetActive(false);
        ControlContainer.SetActive(true);
    }
    public void MainMenuButton()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("");
    }

    public void DeskTopButton()
    {
        
    }
}
