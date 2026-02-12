using UnityEngine;

public class ImageTranslation : MonoBehaviour
{
    [SerializeField] public GameObject GlitchOne;
    [SerializeField] public GameObject GlitchTwo;
    

    public void  DestroyGlitchOne() 
    {
        Destroy(GlitchOne);
    }

    public void DestroyGlitchTwo()
    {
        Destroy(GlitchTwo);
    }
    
    public void  FadeGlitchOne() 
    {
    }
    
    public void  FadeGlitchTwo() 
    {
    }
}
