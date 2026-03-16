using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using PlayerStateType = ISUGameDev.SpearGame.BasePlayerState.PlayerStateType;
    
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private GameObject dialogueUIPrefab;
    [SerializeField] private PlayerStateType dialogueState;
    
    //player state cache vars
    private PlayerStateMachine playerStateMachine;
    private PlayerStateType previousState;

    private void Awake()
    {
        playerStateMachine = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>();
    }

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

    private void OnDialogueUIClosed()
    {
        //return control back to player
        playerStateMachine.ChangeState(previousState);
    }
}
