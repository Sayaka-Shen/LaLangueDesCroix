using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum MessagesType
{
    Sender, 
    Receiver,
    Image
}

public class Message : MonoBehaviour
{
    [Header("Message Properties")]
    [SerializeField] private TextMeshProUGUI m_messageContent;
    [SerializeField] private MessagesType mMessageType;

    public void SetMessageText(string msg)
    {
        m_messageContent.text = msg;
    }

    public void Start()
    {
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        yield return new WaitForEndOfFrame();
        
        //Get rect transforms
        RectTransform parent = transform.parent.GetComponent<RectTransform>();;
        RectTransform rt = this.GetComponent<RectTransform>();
        if (!parent || !rt) yield break;
        
        //We want the message to take only 70% of the width
        float targetWidth = parent.rect.width * 0.7f;
        rt.sizeDelta = new Vector2(targetWidth, rt.sizeDelta.y);
    }
    void LateUpdate()
    {
        
    }

    public string GetMessageText()
    {
        return m_messageContent.text;
    }

    public MessagesType GetMessageOwner()
    {
        return mMessageType;
    }
}
