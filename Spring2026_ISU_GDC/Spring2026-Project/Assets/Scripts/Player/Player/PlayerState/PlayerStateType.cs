namespace ISUGameDev.SpearGame.Player.PlayerState
{
	/// <summary>
	/// Represents all types of states a player can be in for combat, dialogue, menu, etc.
	/// </summary>
	public enum PlayerStateType
	{
		None,
		RoamingWithSpear,
		AimingSpear,
		RoamingWithoutSpear,
		DashingTowardsSpear,
		InDialogue,
		Stunned
	}
}