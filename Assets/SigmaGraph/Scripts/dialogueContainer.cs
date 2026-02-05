using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using static UnityEditor.Rendering.MaterialUpgrader;
#endif

public class dialogueContainer : MonoBehaviour
{
    [Header("Message")]
    [SerializeField] private GameObject m_receiverPrefab;
    [SerializeField] private GameObject m_senderPrefab;
    private RelocateScrollView m_relocateScrollView;
    private GameObject m_messageInstance;
    private RectTransform m_scrollContainerTransform;

    //[SerializeField] private TextMeshProUGUI dialogueText;
    //[SerializeField] private TextMeshProUGUI speakerNameText;
    //[SerializeField] private Image traductionImage;

    private void Start()
    {
        m_scrollContainerTransform = GetComponent<RectTransform>();
        m_relocateScrollView = GetComponent<RelocateScrollView>();
    }

    public void InitializeDialogueContainer(string dialogue, Sprite tradImg, Espeaker speakers /* Sprite traductionImg */)
    {
        //var childContainer = transform.GetChild(0);
        //if (childContainer == null) return;
        //childContainer.gameObject.SetActive(true);

        m_messageInstance = Instantiate(speakers == Espeaker.Toi ? m_receiverPrefab : m_senderPrefab, this.transform);

        if (m_messageInstance == null)
        {
            Debug.Log("Le message prefab n'existe pas.");
            return;
        }

        Message message = m_messageInstance.GetComponentInChildren<Message>();
        if (message != null)
        {
            if (tradImg != null)
            {
                message.SetImage(tradImg);
            }
            else
            {
                if (!string.IsNullOrEmpty(dialogue))
                {
                    message.SetMessageText(dialogue);
                }
            }
        }
        else
        {
            Debug.Log("Il n'y a pas de composant Message.");
        }

        StartCoroutine(ScrollNextFrame());

        //traductionImage.sprite = traductionImg;
        //dialogueText.SetText(dialogue);
        //speakerNameText.SetText(speakerName);
    }

    private IEnumerator ScrollNextFrame()
    {
        yield return new WaitForSeconds(.1f);

        if (m_scrollContainerTransform.sizeDelta.y > 0)
        {
            m_relocateScrollView.UpdateScrollView();
        }
    }

    //public void HideContainer()
    //{
    //    var childContainer = transform.GetChild(0);
    //    if (childContainer == null) return;
    //    childContainer.gameObject.SetActive(false);
    //}
}
