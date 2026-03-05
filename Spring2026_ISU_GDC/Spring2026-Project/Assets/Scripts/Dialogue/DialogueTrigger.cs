using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using PlayerStateType = ISUGameDev.SpearGame.BasePlayerState.PlayerStateType;
    
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private BoxCollider2D triggerZone;
    [SerializeField] private GameObject player;
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private Image characterIconImage;
    [SerializeField] private float textSpeed = 0.05f;
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
        dialogueCanvas.enabled = true;
        
        if (characterNameText != null && !string.IsNullOrEmpty(dialogue.characterName))
        {
            characterNameText.text = dialogue.characterName;
        }
        
        if (characterIconImage != null && dialogue.characterIcon != null)
        {
            characterIconImage.sprite = dialogue.characterIcon;
            characterIconImage.gameObject.SetActive(true);
        }
        
        
        foreach (string sentence in dialogue.dialogueStrings)
        {
            dialogueText.text = "";
            yield return StartCoroutine(TypeSentence(sentence));
            
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        HandleDialogueOver();
    }
    
    IEnumerator TypeSentence(string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void HandleDialogueOver() 
    {
        player.GetComponent<PlayerStateMachine>().ChangeState(PlayerStateType.RoamingWithSpear);
        
        if (dialogueText != null)
            dialogueText.text = "";
        if (characterNameText != null)
            characterNameText.text = "";
        if (characterIconImage != null)
            characterIconImage.gameObject.SetActive(false);
            
        dialogueCanvas.enabled = false;
    }
    public void Start() {
        dialogueCanvas.enabled = false;
    }
    public void Update() 
    {
        if (triggerZone.IsTouching(player.GetComponent<Collider2D>()) && !dialogueTriggered)
        {
            StartDialogue();
        }
    }
}
