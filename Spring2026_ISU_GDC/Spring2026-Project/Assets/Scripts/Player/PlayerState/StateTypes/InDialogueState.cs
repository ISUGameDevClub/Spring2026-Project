using ISUGameDev.SpearGame;
using UnityEngine;
using static ISUGameDev.SpearGame.BasePlayerState;

public class InDialogueState : BasePlayerState
{

    private void Awake()
    {
        this.playerStateType = PlayerStateType.InDialogue;
    }

    public override void ApplyMovementModiferForState(PlayerMovement playerMovement)
    { 
       playerMovement.StopAllCurrentMovement();
       playerMovement.enabled = false;
    }
}
