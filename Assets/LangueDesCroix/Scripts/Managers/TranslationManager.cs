using JetBrains.Annotations;
using UnityEngine;

public enum LANGUAGE
{
    French,
    English
}

public class TranslationManager : MonoBehaviour
{
    public static TranslationManager Instance { get; private set; }
    public LANGUAGE actualLanguage = LANGUAGE.French;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }
    
    public LANGUAGE GetActualLanguage()
    {
        return actualLanguage;
    }
}

