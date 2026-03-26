using ISUGameDev.SpearGame.Player.Movement;
using UnityEngine;

namespace ISUGameDev.SpearGame.Player.PlayerState
{
    /// <summary>
    /// An abstract class representing a PlayerState in game. Sub classes representing states will inherit from this class
    /// to customize PlayerState behaviour regarding what attacks are available, what movement is allowed, etc.
    /// </summary>
    public abstract class BasePlayerState : MonoBehaviour
    {
        /// <summary>
        /// The player state type for this object. If not defined, returns PlayerStateType.None
        /// </summary>
        public PlayerStateType playerStateType { get; protected set; } = PlayerStateType.None;

        /// <summary>
        /// Gets player's assigned (Primary) AttackMechanic delegate for state. 
        /// </summary>
        /// <returns>Null by Default, The AttackMechanic for this state otherwise. If null, assume no attack exists for this state.</returns>
        public virtual AttackMechanic GetPrimaryAttackMechanicForState()
        {
            //by default, return null
            return null;
        }

        /// <summary>
        /// Gets player's assigned (Secondary) AttackMechanic delegate for state. 
        /// </summary>
        /// <returns>Null by Default, The AttackMechanic for this state otherwise. If null, assume no attack exists for this state.</returns>
        public virtual AttackMechanic GetSecondaryAttackMechanicForState()
        {
            //by default, return null
            return null;
        }

        /// <summary>
        /// Applies the movement modifier to the Player for this specific state, if one exists.
        /// </summary>
        public virtual void ApplyMovementModiferForState(PlayerMovement playerMovement)
        {
            //by default, make no changes to player movement and make sure movement is enabled
            playerMovement.enabled = true;
            return;
        }
    }
}

