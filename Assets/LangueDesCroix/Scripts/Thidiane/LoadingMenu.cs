using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    }


    IEnumerator LoadOther()
    {
        while (loadingBarImage.value <= 1f)
        {
            loadingBarImage.value += Time.deltaTime / loadingDuration;
            yield return null;
        }
        isLoading = true;
        //SceneManager.LoadScene("MenuPrincipal");
    }

    
    
}
