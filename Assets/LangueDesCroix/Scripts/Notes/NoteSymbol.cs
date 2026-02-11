using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteSymbol : MonoBehaviour
{
    [Header("Initialization")]
    [SerializeField] private Image imgSymbol;
    [SerializeField] private TextMeshProUGUI txtSymbolTranslation;

    public void Initialize(Sprite newImage, String newText)
    {
        imgSymbol.sprite = newImage;
        txtSymbolTranslation.text = newText;
    }
}
