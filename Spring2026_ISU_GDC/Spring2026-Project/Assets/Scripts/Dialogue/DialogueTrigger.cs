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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;
        
        FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);
        GetComponent<Collider2D>().enabled = false;
    }
}
