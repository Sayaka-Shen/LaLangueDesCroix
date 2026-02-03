using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Rendering.MaterialUpgrader;

public class DialogueContainer : MonoBehaviour
{
    [Header("Message")]
    [SerializeField] private GameObject m_messagePrefab;
    private GameObject m_messageInstance;

    //[SerializeField] private TextMeshProUGUI dialogueText;
    //[SerializeField] private TextMeshProUGUI speakerNameText;
    //[SerializeField] private Image traductionImage;

    public void InitializeDialogueContainer(string dialogue, string speakerName/*Sprite traductionImg */)
    {
        //var childContainer = transform.GetChild(0);
        //if (childContainer == null) return;
        //childContainer.gameObject.SetActive(true);


        m_messageInstance = Instantiate(m_messagePrefab, this.transform);
        if(m_messageInstance == null )
        {
            Debug.Log("Le message prefab n'existe pas.");
            return;
        }

        if(m_messageInstance.TryGetComponent<Message>(out var message))
        {
            message.SetMessageText(dialogue);
        }

        //traductionImage.sprite = traductionImg;
        //dialogueText.SetText(dialogue);
        //speakerNameText.SetText(speakerName);
    }
    
    //public void HideContainer()
    //{
    //    var childContainer = transform.GetChild(0);
    //    if (childContainer == null) return;
    //    childContainer.gameObject.SetActive(false);
    //}
}
