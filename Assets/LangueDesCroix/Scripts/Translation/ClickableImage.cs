using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

[System.Serializable]
public struct ImageInfos
{
    [Header("Sprites")]
    [SerializeField] public IMAGES image;
    [SerializeField] public SYMBOLS symbolOne;
    [SerializeField] public SYMBOLS symbolTwo;
    
    [Header("Mots [FR/ENG]")]
    [SerializeField] public String[] wordsOne;
    [SerializeField] public String[] wordsTwo;
    
    [Header("Indices Mot 1")]
    [SerializeField] public int[] indexRevealWordOne;
    [SerializeField] public int[] attemptsRevealWordOne;
    
    [Header("Indices Mot 2")]
    [SerializeField] public int[] indexRevealWordTwo;
    [SerializeField] public int[] attemptsRevealWordTwo;

    [Header("Dialog Infos")] [SerializeField]
    public int messageIndexToReplace;
    
    [HideInInspector] public String wordOne;
    [HideInInspector] public String wordTwo;
    [HideInInspector] public bool foundWordOne;
    [HideInInspector] public bool foundWordTwo;
    [HideInInspector] public int attemptsWordOne;
    [HideInInspector] public int attemptsWordTwo;
    [HideInInspector] public GameObject parentMessage;
    [HideInInspector] public String messageDecrypted;
}

public class ClickableImage : MonoBehaviour, IPointerClickHandler
{
    public ImageInfos infos;
    [HideInInspector] public ClickableImage twin;

    public void Awake()
    {
        infos.wordOne = infos.wordsOne[0];
        infos.wordTwo = infos.wordsTwo[0];
        
        switch (TranslationManager.Instance.GetActualLanguage())
        {
            case LANGUAGE.French:
                infos.wordOne = infos.wordsOne[0];
                infos.wordTwo = infos.wordsTwo[0];
                break;
            case LANGUAGE.English:
                infos.wordOne = infos.wordsOne[1];
                infos.wordTwo = infos.wordsTwo[1];
                break;
            default :
                infos.wordOne = infos.wordsOne[0];
                infos.wordTwo = infos.wordsTwo[0];
                break;
        }
    }
    public void Initialize(ImageInfos infos)
    {
        this.infos = infos;
        if (gameObject.TryGetComponent<Image>(out Image img))
        {
            img.sprite = PrefabsManager.Instance.GetImage(infos.image);
        }
    }

    public void InitializeTwo(GameObject parentMessage, String  messageDecrypted )
    {
        this.infos.parentMessage = parentMessage;
        this.infos.messageDecrypted = messageDecrypted;
    }
    
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameObject newGO = Instantiate(PrefabsManager.Instance.GetPrefabTraduction(), FindFirstObjectByType<Canvas>().transform);
        if(newGO.TryGetComponent<Translation>(out Translation translation))
        {
            translation.Initialize(this);
        }
    }

    public void fillTwinInfos()
    {
        if (twin != null)
        {
            twin.infos = this.infos;
        }
    }
}
