//This file will contain publicly available delegate types to use in codebase for the player.

namespace ISUGameDev.SpearGame.Player.PlayerState
{
	// this should either be renamed, or moved, because it has everything to do with attacks, and almost nothing to do with state.
	// we need to separate attack data from player state.
	public delegate void AttackMechanic();
}