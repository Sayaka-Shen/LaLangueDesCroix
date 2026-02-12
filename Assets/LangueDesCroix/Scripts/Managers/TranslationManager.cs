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

    public LANGUAGE m_currentLanguage = LANGUAGE.French;

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
    
    public LANGUAGE GetActualLanguage()
    {
        return m_currentLanguage;
    }

    public void SetCurrentLanguage(LANGUAGE language)
    {
        m_currentLanguage = language;
    }
}

