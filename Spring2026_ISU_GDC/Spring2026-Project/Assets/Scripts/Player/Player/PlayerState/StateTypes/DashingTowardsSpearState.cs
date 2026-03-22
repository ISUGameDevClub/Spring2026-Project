using ISUGameDev.SpearGame.Player.Movement;

namespace ISUGameDev.SpearGame.Player.PlayerState.StateTypes
{
    public class DashingTowardsSpearState : BasePlayerState
    {
        private void Awake()
        {
            playerStateType = PlayerStateType.DashingTowardsSpear;
        }

        public override void ApplyMovementModiferForState(PlayerMovement playerMovement)
        {
            playerMovement.enabled = false;
        }
    }
}