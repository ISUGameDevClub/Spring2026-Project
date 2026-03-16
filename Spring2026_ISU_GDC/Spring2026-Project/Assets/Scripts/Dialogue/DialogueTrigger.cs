using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using PlayerStateType = ISUGameDev.SpearGame.BasePlayerState.PlayerStateType;

/// <summary>
/// The DialogueTrigger class is responsible for triggering dialogue events within a Unity scene.
/// It serves as a mechanism to invoke dialogue-related functionalities, such as displaying conversations
/// or interacting with dialogue systems, when specific conditions or events occur.
/// </summary>
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;

    /// <summary>
    /// A reference to the prefab used to create the dialogue UI in the game.
    /// This prefab is instantiated during interactions that trigger dialogue events
    /// and is responsible for displaying dialogue content to the player.
    /// </summary>
    [SerializeField] private GameObject dialogueUIPrefab;
    [SerializeField] private PlayerStateType dialogueState;
    
    //player state cache vars
    /// <summary>
    /// A reference to the player's state machine, which manages and transitions
    /// the player's current state in different gameplay scenarios.
    /// </summary>
    private PlayerStateMachine playerStateMachine;
    private PlayerStateType previousState;

    /// <summary>
    /// Initializes the DialogueTrigger component by caching a reference to the PlayerStateMachine
    /// of the player object. This is done by finding the GameObject tagged as "Player"
    /// and retrieving its PlayerStateMachine component.
    /// This method is called once when the component is loaded.
    /// </summary>
    private void Awake()
    {
        playerStateMachine = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>();
    }

    /// <summary>
    /// Triggered when another Collider2D enters the trigger collider attached
    /// to the GameObject. Used to initiate dialogue and change the player's state
    /// to a dialogue state.
    /// </summary>
    /// <param name="other">The 2D collider that enters the trigger collider.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;
        
        //cache previous state
        previousState = playerStateMachine.currentState.playerStateType;
        
        //Change player's state to dialogue state
        playerStateMachine.ChangeState(dialogueState);
        
        //Create a dialogue UI instance, and initiate dialogue
        var dialogueUI = Instantiate(dialogueUIPrefab).GetComponent<DialogueUI>();
        
        dialogueUI.StartDialogue(dialogue);
        dialogueUI.OnDialogueUIClosed += OnDialogueUIClosed;
        //Disable collider
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// Handles the event triggered when the dialogue UI is closed.
    /// This method restores the player's state to the previous state
    /// that was cached before the dialogue began, returning control to the player.
    /// </summary>
    private void OnDialogueUIClosed()
    {
        //return control back to player
        playerStateMachine.ChangeState(previousState);
    }
}
