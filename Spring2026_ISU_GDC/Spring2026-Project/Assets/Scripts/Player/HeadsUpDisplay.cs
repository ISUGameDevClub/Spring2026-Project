using ISUGameDev.SpearGame.Dialogue;
using Nomad.Core.Events;
using Nomad.Events.Globals;
using UnityEngine;

namespace ISUGameDev.SpearGame.Player
{
	/// <summary>
	/// Player UI composite for delegating behaviors
	/// </summary>
	public class HeadsUpDisplay : MonoBehaviour
	{
		[SerializeField]
		private GameObject dialogueUIPrefab;
		[SerializeField]
		private PlayerStateMachine stateMachine;

		private ISubscriptionHandle onDialogueTriggered;
		private ISubscriptionHandle onDialogueFinished;

		/// <summary>
		/// 
		/// </summary>
		private void Start()
		{
			var dialogueTriggered = GameEventRegistry.GetEvent<DialogueTriggeredEventArgs>(nameof(DialogueTrigger.DialogueTriggered), nameof(DialogueTrigger));
			onDialogueTriggered = dialogueTriggered.Subscribe(OnDialogueTriggered);

			var dialogueFinished = GameEventRegistry.GetEvent<EmptyEventArgs>(nameof(DialogueTrigger.DialogueFinished), nameof(DialogueTrigger));
			onDialogueFinished = dialogueFinished.Subscribe(OnDialogueFinished);
		}

		/// <summary>
		/// 
		/// </summary>
		private void OnDestroy()
		{
			onDialogueTriggered?.Dispose();
			onDialogueFinished?.Dispose();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		private void OnDialogueTriggered(in DialogueTriggeredEventArgs args)
		{
			var dialogueUI = Instantiate(dialogueUIPrefab).GetComponent<DialogueUI>();
			stateMachine.ChangeState(PlayerState.PlayerStateType.InDialogue);
			dialogueUI.StartDialogue(args.Resource);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		private void OnDialogueFinished(in EmptyEventArgs args)
		{
			stateMachine.RestoreState();
		}
	}
}