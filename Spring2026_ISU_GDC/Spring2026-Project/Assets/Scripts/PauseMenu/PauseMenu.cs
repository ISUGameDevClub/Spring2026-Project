using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject MenuContainer;
    [SerializeField] GameObject SettingsContainer;
    [SerializeField] GameObject ControlContainer;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuContainer.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ResumeButton()
    {
        MenuContainer.SetActive(false);
        Time.timeScale = 1.0f;
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
