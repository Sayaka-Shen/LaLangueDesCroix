using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    [Header("Message Properties")]
    [SerializeField] private TextMeshProUGUI m_messageContent;

    public void SetMessageText(string msg)
    {
        m_messageContent.text = msg;
    }
}
