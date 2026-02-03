using TMPro;
using UnityEngine;


public enum MessageOwner
{
    Sender, 
    Receiver
}

public class Message : MonoBehaviour
{
    [Header("Message Properties")]
    [SerializeField] private TextMeshProUGUI m_messageContent;
    [SerializeField] private MessageOwner m_messageOwner;

    public void SetMessageText(string msg)
    {
        m_messageContent.text = msg;
    }

    public string GetMessageText()
    {
        return m_messageContent.text;
    }

    public MessageOwner GetMessageOwner()
    {
        return m_messageOwner;
    }
}
