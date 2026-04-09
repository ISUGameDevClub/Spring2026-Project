using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ISUGameDev.SpearGame
{
    /// <summary>
    /// Common base button class
    /// </summary>
    public class Button : MonoBehaviour, ISelectHandler
    {
        [SerializeField]
        private EventReference focusedSfx;
        [SerializeField]
        private EventReference pressedSfx;
        [SerializeField]
        private UnityEngine.UI.Button button;

		private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            RuntimeManager.PlayOneShot(pressedSfx);
        }

        public void OnSelect(BaseEventData eventData)
        {
            RuntimeManager.PlayOneShot(focusedSfx);
        }
    }
}