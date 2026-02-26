using UnityEngine;
using System.Collections;
using PlayerStateType = ISUGameDev.SpearGame.BasePlayerState.PlayerStateType;
    
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private BoxCollider2D triggerZone;
    [SerializeField] private GameObject player;

    private bool dialogueTriggered = false;
    private PlayerStateType previousState;

    public void StartDialogue()
    {
        dialogueTriggered = true;

        player.GetComponent<PlayerStateMachine>().ChangeState(PlayerStateType.InDialogue);
        StartCoroutine(DisplayDialogue());
    }

    IEnumerator DisplayDialogue()
    {
        foreach (string sentence in dialogue.dialogueStrings)
        {
            Debug.Log(sentence);
            yield return new WaitForSeconds(0.5f); // wait 0.5 seconds
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        HandleDialogueOver();
    }

    public void HandleDialogueOver() {
        player.GetComponent<PlayerStateMachine>().ChangeState(PlayerStateType.RoamingWithSpear);
    }

    public void Update() 
    {
        if (triggerZone.IsTouching(player.GetComponent<Collider2D>()) && !dialogueTriggered)
        {
            StartDialogue();
        }
    }
}
