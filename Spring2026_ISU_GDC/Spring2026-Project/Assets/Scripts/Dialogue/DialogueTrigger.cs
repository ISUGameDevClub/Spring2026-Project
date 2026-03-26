using Nomad.Core.Events;
using Nomad.Events.Globals;
using UnityEngine;

namespace ISUGameDev.SpearGame.Dialogue
{
    /// <summary>
    /// The DialogueTrigger class is responsible for triggering dialogue events within a Unity scene.
    /// It serves as a mechanism to invoke dialogue-related functionalities, such as displaying conversations
    /// or interacting with dialogue systems, when specific conditions or events occur.
    /// </summary>
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField]
        private Dialogue dialogue;

        public IGameEvent<DialogueTriggeredEventArgs> DialogueTriggered => dialogueTriggered;
        private IGameEvent<DialogueTriggeredEventArgs> dialogueTriggered;

        public IGameEvent<EmptyEventArgs> DialogueFinished => dialogueFinished;
        private IGameEvent<EmptyEventArgs> dialogueFinished;

        /// <summary>
        /// Initializes the DialogueTrigger component by caching a reference to the PlayerStateMachine
        /// of the player object. This is done by finding the GameObject tagged as "Player"
        /// and retrieving its PlayerStateMachine component.
        /// This method is called once when the component is loaded.
        /// </summary>
        private void Start()
        {
            dialogueTriggered = GameEventRegistry.GetEvent<DialogueTriggeredEventArgs>(nameof(DialogueTriggered), nameof(DialogueTrigger));
            dialogueFinished = GameEventRegistry.GetEvent<EmptyEventArgs>(nameof(DialogueFinished), nameof(DialogueTrigger));
        }

        /// <summary>
        /// Triggered when another Collider2D enters the trigger collider attached
        /// to the GameObject. Used to initiate dialogue and change the player's state
        /// to a dialogue state.
        /// </summary>
        /// <param name="other">The 2D collider that enters the trigger collider.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            Debug.Log("Dialogue triggered");

            dialogueTriggered.Publish(new DialogueTriggeredEventArgs(dialogue));
            
            // disable collider
            GetComponent<Collider2D>().enabled = false;
            // [SIGAAMDAD] shouldn't we just destroy this object upon usage?
        }
    }
}