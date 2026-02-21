using ISUGameDev.SpearGame;
using UnityEngine;
using static ISUGameDev.SpearGame.BasePlayerState;

public class AimingSpearState : BasePlayerState
{

    private void Awake()
    {
        this.playerStateType = PlayerStateType.AimingSpear;
    }

    public override void ApplyMovementModiferForState(PlayerMovement playerMovement)
    {
       playerMovement.enabled = false;
    }
}
