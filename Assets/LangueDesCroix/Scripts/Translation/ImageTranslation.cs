using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ImageTranslation : MonoBehaviour
{
    [SerializeField] public GameObject GlitchOne;
    [SerializeField] public GameObject GlitchTwo;
    [SerializeField] public CanvasGroup GlitchOneCG;
    [SerializeField] public CanvasGroup GlitchTwoCG;
    

    public void  DestroyGlitchOne() 
    {
        if (GlitchOne) Destroy(GlitchOne);
    }

    public void DestroyGlitchTwo()
    {
        if (GlitchTwo) Destroy(GlitchTwo);
    }
    
    public void  FadeGlitchOne() 
    {
        if (GlitchOneCG) GlitchOneCG.DOFade(0f, 1f);
    }
    
    public void  FadeGlitchTwo() 
    {
        if (GlitchTwoCG) GlitchTwoCG.DOFade(0f, 1f);
    }
}
