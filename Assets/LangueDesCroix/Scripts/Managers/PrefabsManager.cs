using System;
using JetBrains.Annotations;
using UnityEngine;

public enum SYMBOLS
{
    Translate,
    Language,
    Sinister,
    Eye,
    Understand,
    Control,
    Hold,
    Path,
    Two,
    Lost,
    Disappear,
    Null
}

public enum IMAGES
{
    FirstImage,
    SecondImage,
    ThirdImage,
    FourthImage,
    FifthImage,
    SixthImage
}

public class PrefabsManager : MonoBehaviour
{
    public static PrefabsManager Instance { get; private set; }
    
    [Header("Symbols Sprites")]
    [SerializeField] private Sprite SymbolTranslate;
    [SerializeField] private Sprite SymbolLanguage;
    [SerializeField] private Sprite SymbolSinister;
    [SerializeField] private Sprite SymbolEye;
    [SerializeField] private Sprite SymbolUnderstand;
    [SerializeField] private Sprite SymbolControl;
    [SerializeField] private Sprite SymbolHold;
    [SerializeField] private Sprite SymbolPath;
    [SerializeField] private Sprite SymbolTwo;
    [SerializeField] private Sprite SymbolLost;
    [SerializeField] private Sprite SymbolDisappear;
    
    [Header("Images Sprites")]
    [SerializeField] private Sprite FirstImage;
    [SerializeField] private Sprite SecondImage;
    [SerializeField] private Sprite ThirdImage;
    [SerializeField] private Sprite FourthImage;
    [SerializeField] private Sprite FifthImage;
    [SerializeField] private Sprite SixthImage;
    
    [Header("Images Prefabs")]
    [SerializeField] private GameObject FirstImagePrefab;
    [SerializeField] private GameObject SecondImagePrefab;
    [SerializeField] private GameObject ThirdImagePrefab;
    [SerializeField] private GameObject FourthImagePrefab;
    [SerializeField] private GameObject FifthImagePrefab;
    [SerializeField] private GameObject SixthImagePrefab;
    
    [Header("Images Traduction")] 
    [SerializeField] private GameObject FirstImageTrad;
    [SerializeField] private GameObject SecondImageTrad;
    [SerializeField] private GameObject ThirdImageTrad;
    [SerializeField] private GameObject FourthImageTrad;
    [SerializeField] private GameObject FifthImageTrad;
    [SerializeField] private GameObject SixthImageTrad;

    [Header("Other Prefabs")]
    [SerializeField] private GameObject prefabTraduction;
    [SerializeField] public GameObject prefabNoteSymbol;
    [SerializeField] public GameObject prefabNoteSentence;
    [SerializeField] public GameObject goNoteContainer;
    public DialogueManager dialogueManager;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public GameObject GetPrefabTraduction()
    {
        return prefabTraduction;
    }

    public Sprite GetSymbol(SYMBOLS symbol)
    {
        switch (symbol)
        {
            case SYMBOLS.Translate :
                return SymbolTranslate;
            case SYMBOLS.Language :
                return SymbolLanguage;
            case SYMBOLS.Sinister :
                return SymbolSinister;
            case SYMBOLS.Eye :
                return SymbolEye;
            case SYMBOLS.Understand :
                return SymbolUnderstand;
            case SYMBOLS.Control :
                return SymbolControl;
            case SYMBOLS.Hold :
                return SymbolHold;
            case SYMBOLS.Path :
                return SymbolPath;
            case SYMBOLS.Two :
                return SymbolTwo;
            case SYMBOLS.Lost :
                return SymbolLost;
            case SYMBOLS.Disappear :
                return SymbolDisappear;
            case SYMBOLS.Null :
                return null;
            default :
                return null;
        }
    }

    public String GetStringFromSymbol(SYMBOLS symbol)
    {
        switch (symbol)
        {
            case SYMBOLS.Translate :
                return "<sprite=1>";
            case SYMBOLS.Language :
                return "<sprite=0>";
            case SYMBOLS.Sinister :
                return "<sprite=3>";
            case SYMBOLS.Eye :
                return "<sprite=2>";
            case SYMBOLS.Understand :
                return "<sprite=4>";
            case SYMBOLS.Control :
                return "<sprite=7>";
            case SYMBOLS.Hold :
                return "<sprite=5>";
            case SYMBOLS.Path :
                return "<sprite=6>";
            case SYMBOLS.Two :
                return "<sprite=9>";
            case SYMBOLS.Lost :
                return "<sprite=8>";
            case SYMBOLS.Disappear :
                return "<sprite=10>";
            case SYMBOLS.Null :
                return "";
            default :
                return "";
        }
    }
    
    public Sprite GetImage(IMAGES images)
    {
        switch (images)
        {
            case IMAGES.FirstImage :
                return FirstImage;
            case IMAGES.SecondImage :
                return SecondImage;
            case IMAGES.ThirdImage :
                return ThirdImage;
            case IMAGES.FourthImage :
                return FourthImage;
            case IMAGES.FifthImage :
                return FifthImage;
            case IMAGES.SixthImage :
                return SixthImage;
            default:
                return null;
        }
    }
    
    public GameObject GetImageTrad(IMAGES images)
    {
        switch (images)
        {
            case IMAGES.FirstImage :
                return FirstImageTrad;
            case IMAGES.SecondImage :
                return SecondImageTrad;
            case IMAGES.ThirdImage :
                return ThirdImageTrad;
            case IMAGES.FourthImage :
                return FourthImageTrad;
            case IMAGES.FifthImage :
                return FifthImageTrad;
            case IMAGES.SixthImage :
                return SixthImageTrad;
            default:
                return null;
        }
    }
    

    [NotNull]
    public GameObject GetPrefabBasedOnSprite(Sprite sprite)
    {
        if (sprite == FirstImage)
        {
            return FirstImagePrefab;
        }
        else if (sprite == SecondImage)
        {
            return SecondImagePrefab;
        }
        else if (sprite == ThirdImage)
        {
            return ThirdImagePrefab;
        }
        else if (sprite == FourthImage)
        {
            return FourthImagePrefab;
        }
        else if (sprite == FifthImage)
        {
            return FifthImagePrefab;
        } 
        else if (sprite == SixthImage)
        {
            return SixthImagePrefab;
        }
        else
        {
            return null;
        }
    }
    
}

