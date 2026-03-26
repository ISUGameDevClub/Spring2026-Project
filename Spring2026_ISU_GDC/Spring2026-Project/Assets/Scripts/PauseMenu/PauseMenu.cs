using System;
using FrameworkHandler.State;
using Nomad.Core.Engine.Globals;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuContainer;

    [SerializeField]
    private GameObject SettingsContainer;

    [SerializeField]
    private GameObject ControlContainer;

    [SerializeField]
    private InputActionReference _pauseAction;

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        GameStateManager.GameStateChanged.Subscribe(OnGameStateChanged);
        _pauseAction.action.started += OnPauseActionTriggered;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
	private void OnPauseActionTriggered(InputAction.CallbackContext context)
    {
        GameStateManager.Instance.SetGameState(GameState.Paused);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
	private void OnGameStateChanged(in GameStateChangedEventArgs args)
    {
        if (args.NewState == GameState.Paused)
        {
            OpenMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void OpenMenu()
    {
        Time.timeScale = 0.0f;
        MenuContainer.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    private void CloseMenu()
    {
        Time.timeScale = 1.0f;
        MenuContainer.SetActive(false);
        GameStateManager.Instance.SetGameState(GameState.Level);
        Destroy(gameObject);
    }
    
    /// <summary>
    /// 
    /// </summary>
    private void ResumeButton()
    {
        CloseMenu();
    }

    /// <summary>
    /// 
    /// </summary>
    private void SettingButton()
    {
        MenuContainer.SetActive(false);
        SettingsContainer.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    private void ControlButton()
    {
        MenuContainer.SetActive(false);
        ControlContainer.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    private void MainMenuButton()
    {
        GameStateManager.Instance.SetGameState(GameState.Menu);
    }

    /// <summary>
    /// 
    /// </summary>
    private void DesktopButton()
    {
        EngineService.Quit();
    }
}
