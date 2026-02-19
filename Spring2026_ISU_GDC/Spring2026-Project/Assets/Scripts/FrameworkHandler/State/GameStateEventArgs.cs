namespace FrameworkHandler.State
{
	/// <summary>
	/// Event that triggers whenever the game state changes.
	/// </summary>
	public readonly struct GameStateChangedEventArgs
	{
		/// <summary>
		/// The previous game state.
		/// </summary>
		public readonly GameState OldState { get; }

		/// <summary>
		/// The current game state.
		/// </summary>
		public readonly GameState NewState { get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="oldState"></param>
		/// <param name="newState"></param>
		public GameStateChangedEventArgs(GameState oldState, GameState newState)
		{
			OldState = oldState;
			NewState = newState;
		}
	}
}