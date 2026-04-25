using FrameworkHandler.State;
using Nomad.Core.Engine.Globals;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
     [Header("Menu Prefabs")]
    [SerializeField] private GameObject settingsMenuPrefab;
    [SerializeField] private GameObject creditsMenuPrefab;

    [Header("Parent Canvas")]
    [SerializeField] private Transform uiParent;

    public void OnNewGamePressed()
    {
        GameStateManager.Instance.SetGameState( GameState.Level );
    }

    public void OnContinueGamePressed()
    {
    }

    // TODO: actually make settings menu a prefab
    public void OnSettingsMenuPressed()
    {
        if (settingsMenuPrefab == null)
        {
            Debug.LogError("Settings menu prefab not assigned!");
            return;
        }

        Instantiate(settingsMenuPrefab, uiParent);
    }

    public void OnCreditsMenuPressed()
    {
        if (creditsMenuPrefab == null)
        {
            Debug.LogError("Credits menu prefab not assigned!");
            return;
        }

        Instantiate(creditsMenuPrefab, uiParent);
        // TODO: hook the credits menu into here
    }

    public void OnQuitGamePressed()
    {
        EngineService.Quit();
    }
}
