using ISUGameDev.SpearGame;
using UnityEngine;

public class DashingTowardsSpearState : BasePlayerState
{
    private void Awake()
    {
        this.playerStateType = PlayerStateType.DashingTowardsSpear;
    }

    public override void ApplyMovementModiferForState(PlayerMovement playerMovement)
    {
        playerMovement.enabled = false;
    }
}
