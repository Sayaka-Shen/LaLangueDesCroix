using JetBrains.Annotations;
using UnityEngine;

public enum SYMBOLS
{
    Lost,
    See
}

public enum IMAGES
{
    FirstImage,
    SecondImage,
    ThirdImage,
    FourthImage,
    FifthImage
}

public class PrefabsManager : MonoBehaviour
{
    public static PrefabsManager Instance { get; private set; }
    
    //Prefabs List
    [SerializeField] private GameObject prefabTraduction;
    
    [SerializeField] private Sprite SymbolLost;
    [SerializeField] private Sprite SymbolSee;
    
    [SerializeField] private Sprite FirstImage;
    [SerializeField] private Sprite SecondImage;
    [SerializeField] private Sprite ThirdImage;
    [SerializeField] private Sprite FourthImage;
    [SerializeField] private Sprite FifthImage;
    [SerializeField] private Sprite SixthImage;
    
    [SerializeField] private GameObject FirstImagePrefab;
    [SerializeField] private GameObject SecondImagePrefab;
    [SerializeField] private GameObject ThirdImagePrefab;
    [SerializeField] private GameObject FourthImagePrefab;
    [SerializeField] private GameObject FifthImagePrefab;
    [SerializeField] private GameObject SixthImagePrefab;
    
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
            case SYMBOLS.Lost :
                return SymbolLost;
            case SYMBOLS.See :
                return SymbolSee;
            default :
                return null;
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

