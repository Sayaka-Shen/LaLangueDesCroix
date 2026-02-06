using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public struct ImageInfos
{
    [SerializeField] public IMAGES image;
    [SerializeField] public SYMBOLS symbolOne;
    [SerializeField] public SYMBOLS symbolTwo;
    [SerializeField] public String wordOne;
    [SerializeField] public String wordTwo;
    [SerializeField] public int[] indexRevealWordOne;
    [SerializeField] public int[] attemptsRevealWordOne;
    [SerializeField] public int[] indexRevealWordTwo;
    [SerializeField] public int[] attemptsRevealWordTwo;
    
    [HideInInspector] public bool foundWordOne;
    [HideInInspector] public bool foundWordTwo;
    [HideInInspector] public int attemptsWordOne;
    [HideInInspector] public int attemptsWordTwo;
}

public class ClickableImage : MonoBehaviour, IPointerClickHandler
{
    public ClickableImage twinScript;
    public ImageInfos infos;
    
    public void Initialize(ImageInfos infos)
    {
        this.infos = infos;
        if (gameObject.TryGetComponent<Image>(out Image img))
        {
            img.sprite = PrefabsManager.Instance.GetImage(infos.image);
        }
        
    }
    
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameObject newGO = Instantiate(PrefabsManager.Instance.GetPrefabTraduction(), FindFirstObjectByType<Canvas>().transform);
        if(newGO.TryGetComponent<Translation>(out Translation translation))
        {
            translation.Initialize(this);
        }
    }
}
