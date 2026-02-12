using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class BoutonUIType : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TypeBouton _type;
    [SerializeField] private UIManager _uiManager;
    public void OnPointerClick(PointerEventData eventData)
    {
        _uiManager.AfficherUI(_type);
        AudioManager.instance.PlaySFX("");
    }


}




