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
        AudioManager.instance.PlaySFX("select_language");
        _uiManager.AfficherUI(_type);
    }


}




