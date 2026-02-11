using TMPro;
using UnityEngine;

public class PopupContainer : MonoBehaviour
{
    [Header("Popup")]
    [SerializeField] private TMP_Text m_popupTxt;

    public void SetPopupTxt(string newTxt)
    {
        m_popupTxt.text = newTxt;
    }
}
