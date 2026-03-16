using ISUGameDev.SpearGame;
using UnityEngine;
using static ISUGameDev.SpearGame.BasePlayerState;

public class GamePausedState : BasePlayerState
{
    private void Awake()
    {
        this.playerStateType = PlayerStateType.GamePaused;
    }

    public override void ApplyMovementModiferForState(PlayerMovement playerMovement)
    { 
        playerMovement.StopAllCurrentMovement();
        playerMovement.enabled = false;
    }
}
