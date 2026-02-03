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
    
    void LateUpdate()
    {
        //Get rect transforms
        RectTransform parent = transform.parent.GetComponent<RectTransform>();;
        RectTransform rt = this.GetComponent<RectTransform>();
        if (!parent || !rt) return;
        
        //We want the message to take only 70% of the width
        float targetWidth = parent.rect.width * 0.7f;
        rt.sizeDelta = new Vector2(targetWidth, rt.sizeDelta.y);
    }
}
