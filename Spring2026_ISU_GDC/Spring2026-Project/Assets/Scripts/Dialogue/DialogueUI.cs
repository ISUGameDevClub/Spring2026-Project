using System;
using System.Collections;
using ISUGameDev.SpearGame.Player;
using ISUGameDev.SpearGame.Player.PlayerState;
using Nomad.Core.Events;
using Nomad.Events.Globals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ISUGameDev.SpearGame.Dialogue
{
    /// <summary>
    /// Manages the display and interaction of dialogue UI elements in the game.
    /// Responsible for initiating dialogue sequences and signaling when the dialogue UI
    /// is closed to enable further game interactions.
    /// </summary>
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private Canvas dialogueCanvas;

        /// <summary>
        /// A reference to the UI text component responsible for displaying
        /// the dialogue text during a dialogue sequence. This variable
        /// is dynamically updated to show the typed-out text corresponding
        /// to the current dialogue sentence.
        /// </summary>
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private TextMeshProUGUI characterNameText;

        /// <summary>
        /// Represents the UI element used to display the character's icon during dialogue sequences.
        /// Typically shows a visual representation of the character currently speaking in the dialogue.
        /// </summary>
        [SerializeField] private Image characterIconImage;
        [SerializeField] private float textSpeed = 0.05f;

        [SerializeField] private Sprite valkyrieOverrideIcon;
        [SerializeField] private Sprite ratatoskrOverrideIcon;

        [SerializeField] private bool capitalizeNames = false;
        
        private IGameEvent<EmptyEventArgs> dialogueFinished;
        private bool skipTyping = false;

        private bool disappearAfterDialogue = false;
        [SerializeField] private GameObject spriteToTurnOff;
        [SerializeField] private Transform spriteToTurnOffPos;
        [SerializeField] private GameObject fogVFX;

        /// <summary>
        /// Unity's Awake method is called when the script instance is being loaded.
        /// Initializes the DialogueUI component by disabling the dialogue canvas at the start,
        /// ensuring that the dialogue UI is hidden by default.
        /// </summary>
        private void Awake()
        {
            dialogueFinished = GameEventRegistry.GetEvent<EmptyEventArgs>(nameof(DialogueTrigger.DialogueFinished), nameof(DialogueTrigger));
            dialogueCanvas.enabled = false;
        }

        private void Start()
        {
            spriteToTurnOff = GameObject.FindWithTag("NPC");

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                skipTyping = true;
            }
        }

        public void StartDialogue(Dialogue dialogue)
        {
            StartCoroutine(DisplayDialogue(dialogue));
        }

        /// <summary>
        /// Displays the dialogue sequence on the dialogue UI by iterating through the sentences provided in the
        /// <see cref="Dialogue"/> object. This method is a coroutine that handles typing out each sentence,
        /// waits for user input to proceed, and manages the setup and cleanup of the dialogue UI components.
        /// </summary>
        /// <param name="dialogue">The <see cref="Dialogue"/> object containing the text, character name, and icon to display.</param>
        /// <returns>An <see cref="IEnumerator"/> used for coroutine execution to support timed events during dialogue rendering.</returns>
        private IEnumerator DisplayDialogue(Dialogue dialogue)
        {
            dialogueCanvas.enabled = true;

            if (characterNameText != null && !string.IsNullOrEmpty(dialogue.characterName))
            {
                characterNameText.text = capitalizeNames ? dialogue.characterName.ToUpper() : dialogue.characterName;
            }

            if (characterIconImage != null && dialogue.characterIcon != null)
            {
                characterIconImage.sprite = dialogue.characterIcon;
                characterIconImage.gameObject.SetActive(true);
            }

            foreach (string sentence in dialogue.dialogueStrings)
            {
                dialogueText.text = string.Empty;

                string processedSentence = sentence;
                if (sentence.StartsWith("[v]"))
                {
                    processedSentence = sentence.Substring(3);
                    characterIconImage.sprite = valkyrieOverrideIcon;
                }
                else if (sentence.StartsWith("[r]"))
                {
                    processedSentence = sentence.Substring(3);
                    characterIconImage.sprite = ratatoskrOverrideIcon;
                }
                else if (sentence.StartsWith("[d]"))
                {
                    processedSentence = sentence.Substring(3);
                    disappearAfterDialogue = true;
                }
                else
                {
                    characterIconImage.sprite = dialogue.characterIcon;
                }

                    yield return StartCoroutine(TypeSentence(processedSentence));

                if (skipTyping)
                {
                    dialogueText.text = processedSentence;
                    skipTyping = false;
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                }
                else
                {
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                }
            }
            HandleDialogueOver();
        }

        /// <summary>
        /// Types out a given sentence letter by letter in the dialogue text component,
        /// creating a typewriter effect for the dialogue display.
        /// </summary>
        /// <param name="sentence">The sentence to be displayed with a typewriter effect.</param>
        /// <returns>
        /// Coroutine enumerator that processes the typewriter effect by gradually
        /// adding each character of the supplied sentence to the text display.
        /// </returns>
        private IEnumerator TypeSentence(string sentence)
        {
            skipTyping = false;
            foreach (char letter in sentence)
            {
                if (skipTyping)
                {
                    dialogueText.text = sentence;
                    yield break;
                }

                dialogueText.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }
        }

        /// <summary>
        /// Handles the termination of a dialogue sequence and resets the dialogue UI elements.
        /// Clears the dialogue text, character name, and character icon, hides the dialogue canvas,
        /// invokes the OnDialogueUIClosed event to signal that the dialogue UI is closed,
        /// and destroys the current dialogue UI game object.
        /// </summary>
        private void HandleDialogueOver()
        {
            if (disappearAfterDialogue)
            {
                GameObject spritePos = GameObject.FindWithTag("NPC");
                Vector3 spawnPosition = spritePos.transform.position;

                spriteToTurnOff.SetActive(false);
                Instantiate(fogVFX, spawnPosition, Quaternion.identity);
            }

            if (dialogueText != null)
            {
                dialogueText.text = string.Empty;
            }
            if (characterNameText != null)
            {
                characterNameText.text = string.Empty;
            }
            if (characterIconImage != null)
            {
                characterIconImage.gameObject.SetActive(false);
            }

            dialogueCanvas.enabled = false;
            
            //bandaid fix
            FindFirstObjectByType<PlayerStateMachine>().ChangeState(PlayerStateType.RoamingWithSpear);
            
            //dialogueFinished.Publish(default);
        }
    }
}