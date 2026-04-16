using ISUGameDev.SpearGame.Player.Movement;

namespace ISUGameDev.SpearGame.Player.PlayerState.StateTypes
{
    public class InDialogueState : BasePlayerState
    {

        private void Awake()
        {
            playerStateType = PlayerStateType.InDialogue;
        }

        public override void ApplyMovementModiferForState(PlayerMovement playerMovement)
        {
            playerMovement.StopAllCurrentMovement();
            playerMovement.enabled = false;
        }
    }
}