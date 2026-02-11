using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingMenu : MonoBehaviour
{
    [SerializeField] private Slider loadingBarImage;

    [SerializeField] private float loadingDuration = 3f;

    [SerializeField] private bool isLoading = false;

    void Start()
    {
        loadingBarImage.value = 0f;
        StartCoroutine(LoadSceneAsync());
    }

    void Update()
    {
        
    }

    public void Chargement()
    {
        StartCoroutine(LoadOther());
    }


    IEnumerator LoadSceneAsync()
    {
        while (loadingBarImage.value <= 0.5f)
        {
            loadingBarImage.value += Time.deltaTime / loadingDuration;
            yield return null;
        }
        isLoading = true;
    }
    IEnumerator LoadOther()
    {
        while (loadingBarImage.value <= 1f)
        {
            loadingBarImage.value += Time.deltaTime / loadingDuration;
            yield return null;
        }
        isLoading = true;
    }

    
    
}
