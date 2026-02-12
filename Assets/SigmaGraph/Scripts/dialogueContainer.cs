using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using static UnityEditor.Rendering.MaterialUpgrader;
#endif

public class dialogueContainer : MonoBehaviour
{
    [Header("PhoneManager")]
    [SerializeField] private PhoneManager m_phoneManager;

    [Header("Gallery")]
    [SerializeField] private GameObject m_galleryContainer;
    
    [Header("Message")]
    [SerializeField] private GameObject m_receiverPrefab;
    [SerializeField] private GameObject m_senderPrefab;
    [SerializeField] private GameObject m_imgPrefab;
    private RelocateScrollView m_relocateScrollView;
    private GameObject m_messageInstance;
    private GameObject m_messageImgInstance;
    private RectTransform m_scrollContainerTransform;

    [Header("PopupTXT")]
    [SerializeField] private GameObject m_popup;

    //[SerializeField] private TextMeshProUGUI dialogueText;
    //[SerializeField] private TextMeshProUGUI speakerNameText;
    //[SerializeField] private Image traductionImage;

    private void Start()
    {
        m_scrollContainerTransform = GetComponent<RectTransform>();
        m_relocateScrollView = GetComponent<RelocateScrollView>();
    }

    public void InitializeDialogueContainer(string dialogue, Sprite tradImg, Espeaker speakers, string popupTxt /* Sprite traductionImg */)
    {
        //var childContainer = transform.GetChild(0);
        //if (childContainer == null) return;
        //childContainer.gameObject.SetActive(true);

        if (!string.IsNullOrEmpty(popupTxt))
        {
            if (m_popup.TryGetComponent<PopupContainer>(out PopupContainer popupContainer))
            {
                popupContainer.SetPopupTxt(popupTxt);
            }

            m_phoneManager.OpenPopup();
        }

        // CHECK ONLY FOR IMAGE TO SPAWN MESSAGE IMAGE PREFAB
        if (tradImg != null)
        {
            m_messageImgInstance = Instantiate(m_imgPrefab, this.transform);
            PrefabsManager.Instance.dialogueManager.IsWaitingForTraduction = true;

            if (m_messageImgInstance == null)
            {
               Debug.Log("Le message image prefab n'existe pas.");
            }

            GameObject m_ImgPrefabInstance = PrefabsManager.Instance.GetPrefabBasedOnSprite(tradImg);
            if (m_ImgPrefabInstance != null)
            {
                GameObject prefabOne = Instantiate(m_ImgPrefabInstance ,m_messageImgInstance.transform.GetChild(0).transform);
                GameObject prefabTwo = Instantiate(m_ImgPrefabInstance ,m_galleryContainer.transform);
                
                ClickableImage firstScript = prefabOne.GetComponent<ClickableImage>();
                ClickableImage secondScript = prefabTwo.GetComponent<ClickableImage>();

                if (firstScript != null && secondScript != null)
                {
                    firstScript.InitializeTwo(m_messageImgInstance, dialogue);
                    secondScript.InitializeTwo(m_messageImgInstance, dialogue);
                    
                    firstScript.twin = secondScript;
                    secondScript.twin = firstScript;
                }
            }
            
            //Instantiate le deuxieme clickabe image dans la gallery puis relier les 2 scripts entre eux

            StartCoroutine(ScrollNextFrame());
            AudioManager.instance.PlaySFX("receive_message");
        }
        // CHECK ONLY FOR TEXT TO SPAWN THE MESSAGE PREFAB
        else
        {
            m_messageInstance = Instantiate(speakers == Espeaker.Joueur ? m_receiverPrefab : m_senderPrefab, this.transform);

            if(speakers == Espeaker.Joueur)
            {
                AudioManager.instance.PlaySFX("send_message");
            }
            else
            {
                AudioManager.instance.PlaySFX("receive_message");
            }


                Message message = m_messageInstance.GetComponentInChildren<Message>();

            if(message == null)
            {
                Debug.Log("Il n'y a pas de composant Message.");
                return;
            }

            if (m_messageInstance == null)
            {
                Debug.Log("Le message prefab n'existe pas.");
                return;
            }

            if (speakers == Espeaker.GAE)
            {
                message.ChangeBgColor(new Color(148.0f / 255.0f, 56.0f / 255.0f, 55.0f / 255.0f));
            }

            if (!string.IsNullOrEmpty(dialogue))
            {
                message.SetMessageText(dialogue);
            }

            StartCoroutine(ScrollNextFrame());
        }


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
