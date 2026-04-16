using System.Linq;
using ISUGameDev.SpearGame.Player.Movement;
using ISUGameDev.SpearGame.Player.PlayerState;
using UnityEngine;

namespace ISUGameDev.SpearGame.Player
{
    /// <summary>
    /// Class to manage the players current state at any given time.
    /// </summary>
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] PlayerMovement movement;
        [SerializeField] PlayerAttackController attacks;
        [SerializeField] private GameObject PlayerStatesParent;

        public BasePlayerState currentState { get; private set; }
        private PlayerEventManager playerEventManager;

        private BasePlayerState prevState;

        void Start()
        {
            playerEventManager = GetComponent<PlayerEventManager>();

            //TODO: Make deciding initial state more data driven, in a table or SO later
            //by default, player state is RoamingWithSpear
            ChangeState(PlayerStateType.RoamingWithSpear);
        }

        /// <summary>
        /// Changes the currentState associated with the player at any given time.
        /// </summary>
        /// <param name="newPlayerState">The new state you want the player in.</param>
        public void ChangeState(PlayerStateType newPlayerStateType)
        {
            BasePlayerState newPlayerState = GetRuntimePlayerStateFromChildObjects(newPlayerStateType);

            if (newPlayerState == null)
            {
                Debug.LogError("Couldn't find player state with name " + newPlayerStateType.ToString() + "in the scene.");
            }

            prevState = currentState;
            currentState = newPlayerState;
            currentState.ApplyMovementModiferForState(movement);

            //Broadcast an event that announces PlayerStateHasBeenChanged
            playerEventManager.OnPlayerStateChanged.Invoke(currentState);
        }

        /// <summary>
        /// Helper function that searches children of PlayerStatesParent for a PlayerState that exists in runtime
        /// </summary>
        private BasePlayerState GetRuntimePlayerStateFromChildObjects(PlayerStateType playerStateType)
        {
            BasePlayerState foundState = GetComponentsInChildren<BasePlayerState>()
                .FirstOrDefault(state => state.playerStateType == playerStateType);

            if (foundState == null)
            {
                Debug.LogWarning($"State {playerStateType} not found in children. Verify PlayerState exists in scene.");
            }

            return foundState;
        }

        /// <summary>
        /// Restores the state used before the most recent <see cref="ChangeState"/> call.
        /// </summary>
        public void RestoreState()
        {
            ChangeState(prevState.playerStateType);
        }
    }
}