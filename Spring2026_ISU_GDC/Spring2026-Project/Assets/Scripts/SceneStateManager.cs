using System;
using FrameworkHandler.State;
using Nomad.Core.Engine.Globals;

public static class SceneStateManager
{
	static SceneStateManager()
	{
		GameStateManager.GameStateChanged.Subscribe(OnGameStateChanged);
	}

	private static void OnGameStateChanged(in GameStateChangedEventArgs args)
	{
		switch (args.NewState)
		{
			case GameState.Level:
				break;
			case GameState.Menu:
				break;
			case GameState.Paused:
				break;
		}
	}
}