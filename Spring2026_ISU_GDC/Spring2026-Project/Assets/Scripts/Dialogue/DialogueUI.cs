using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayerStateType = ISUGameDev.SpearGame.BasePlayerState.PlayerStateType;

public class DialogueUI : MonoBehaviour
{
   //Dialogue menu component references
   [SerializeField] private Canvas dialogueCanvas;
   [SerializeField] private TextMeshProUGUI dialogueText;
   [SerializeField] private TextMeshProUGUI characterNameText;
   [SerializeField] private Image characterIconImage;
   [SerializeField] private float textSpeed = 0.05f;
   
   public event Action OnDialogueUIClosed;
   
   private void Awake()
   {
      
      dialogueCanvas.enabled = false;
   }
   
   public void StartDialogue(Dialogue dialogue)
   {
      StartCoroutine(DisplayDialogue(dialogue));
   }

   private IEnumerator DisplayDialogue(Dialogue dialogue)
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
      foreach (char letter in sentence)
      {
         dialogueText.text += letter;
         yield return new WaitForSeconds(textSpeed);
      }
   }

   private void HandleDialogueOver() 
   {
      if (dialogueText != null)
         dialogueText.text = "";
      if (characterNameText != null)
         characterNameText.text = "";
      if (characterIconImage != null)
         characterIconImage.gameObject.SetActive(false);
            
      dialogueCanvas.enabled = false;
      OnDialogueUIClosed?.Invoke();
      Destroy(gameObject);
   }
}
