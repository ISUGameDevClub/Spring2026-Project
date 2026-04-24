using FrameworkHandler.State;
using Nomad.Core.Engine.Globals;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
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
    }

    public void OnCreditsMenuPressed()
    {
        // TODO: hook the credits menu into here
    }

    public void OnQuitGamePressed()
    {
        EngineService.Quit();
    }
}
