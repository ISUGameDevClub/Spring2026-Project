using ISUGameDev.SpearGame.Player.Movement;
using UnityEngine;

namespace ISUGameDev.SpearGame.Player.PlayerState.StateTypes
{
    public class InDialogueState : BasePlayerState
    {
        [SerializeField] private AnimationClip idleAnimClip;
        
        
        private void Awake()
        {
            playerStateType = PlayerStateType.InDialogue;
        }

        public override void ApplyMovementModiferForState(PlayerMovement playerMovement)
        {
            playerMovement.StopAllCurrentMovement();
            playerMovement.enabled = false;
            
            //bandaid fix hack for animations
            playerMovement.gameObject.GetComponent<Animator>().Rebind();
            playerMovement.gameObject.GetComponent<Animator>().Update(0f);
        }
    }
}