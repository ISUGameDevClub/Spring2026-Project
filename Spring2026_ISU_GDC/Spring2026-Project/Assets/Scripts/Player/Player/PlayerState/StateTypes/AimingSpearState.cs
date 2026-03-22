using ISUGameDev.SpearGame.Player.Movement;

namespace ISUGameDev.SpearGame.Player.PlayerState.StateTypes
{
    public class AimingSpearState : BasePlayerState
    {

        private void Awake()
        {
            playerStateType = PlayerStateType.AimingSpear;
        }

        public override void ApplyMovementModiferForState(PlayerMovement playerMovement)
        {
            playerMovement.enabled = false;
        }
    }
}