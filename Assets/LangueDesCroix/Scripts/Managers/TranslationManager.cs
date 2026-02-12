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

    public language currentLanguage = language.FR;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    
    public language GetActualLanguage()
    {
        return currentLanguage;
    }

    public void SetCurrentLanguage(language language)
    {
        currentLanguage = language;
    }
}

