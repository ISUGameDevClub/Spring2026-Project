using UnityEngine;
using System.Collections.Generic;

namespace ISUGameDev.SpearGame.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Objects/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [Header("Dialogue")]
        public List<string> dialogueStrings = new List<string>();

        [Header("Icon")]
        public Sprite characterIcon;

        [Header("Name")]
        public string characterName;
    }
}