using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DSDialogueSO : ScriptableObject
{
    [field: SerializeField] public string DialogueName { get; set; }
    [field: SerializeField][field: TextArea()] public string Text { get; set; }
    [field: SerializeField] public List<DSDialogueChoiceData> Choices { get; set; }
    [field: SerializeField] public DSDialogueType DialogueType { get; set; }
    [field: SerializeField] public bool IsStartingDialogue { get; set; }
    [field: SerializeField] public Espeaker Speaker { get; set; }
    [field: SerializeField] public Sprite TraductionImage { get; set; }

    public void Initialize(string dialogueName, string text, List<DSDialogueChoiceData> choices, DSDialogueType dialogueType, bool isStartingDialogue, Espeaker espeaker, Sprite tradImg)
    {
        DialogueName = dialogueName;
        Text = text;
        Choices = choices;
        DialogueType = dialogueType;
        IsStartingDialogue = isStartingDialogue;
        Speaker = espeaker;
        TraductionImage = tradImg;
    }
}
