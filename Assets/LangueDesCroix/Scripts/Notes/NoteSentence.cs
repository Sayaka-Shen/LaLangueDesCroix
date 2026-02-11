using System;
using TMPro;
using UnityEngine;

public class NoteSentence : MonoBehaviour
{
    [Header("Initialization")]
    [SerializeField] private TextMeshProUGUI txtSymbols;
    [SerializeField] private TextMeshProUGUI txtSentenceTranslation;

    public void Initialize(String newtxtSymbols, String newTextSentence)
    {
        txtSymbols.text = newtxtSymbols;
        txtSentenceTranslation.text = newTextSentence;
    }
}
