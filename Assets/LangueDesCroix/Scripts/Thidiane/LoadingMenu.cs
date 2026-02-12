using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VectorGraphics;

public class LoadingMenu : MonoBehaviour
{
    [SerializeField] private Slider loadingBarImage;

    [SerializeField] private float loadingDuration = 3f;

    [SerializeField] private bool isLoading = false;


    void Start()
    {
        loadingBarImage.value = 0f;
    }

    void Update()
    {
        
    }

    public void Chargement()
    {
        StartCoroutine(LoadOther());
        AudioManager.instance.PlaySFX("select_language");
    }

    public void SetLanguage(bool isFrench)
    {
        if(isFrench)
        {
            TranslationManager.Instance.SetCurrentLanguage(language.FR);
        }
        else
        {
            TranslationManager.Instance.SetCurrentLanguage(language.EN);
        }

        StartCoroutine(WaitBeforeLoadingScene());
    }

    private IEnumerator WaitBeforeLoadingScene()
    {
        yield return new WaitForSeconds(loadingDuration);
        SceneManager.LoadScene(1);
    }

    IEnumerator LoadOther()
    {
        while (loadingBarImage.value <= 1f)
        {
            loadingBarImage.value += Time.deltaTime / loadingDuration;
            yield return null;
        }
        isLoading = true;
        SceneManager.LoadScene("Merging");
    }

    
    
}
