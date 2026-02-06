using UnityEngine;
using UnityEngine.EventSystems;

public class MessageContainer : MonoBehaviour //, IPointerClickHandler
{
    [Header("Phone Manager")]
    [SerializeField] private PhoneManager m_phoneManager;
    
    //public void OnPointerClick(PointerEventData pointerEventData)
    //{ 
    //    if (m_phoneManager.HasAlreadyClickedDp)
    //    { 
    //        m_phoneManager.CloseDropdownMenu();
    //    }
    //}
}
