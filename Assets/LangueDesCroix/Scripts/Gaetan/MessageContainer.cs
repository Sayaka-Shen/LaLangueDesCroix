using UnityEngine;
using UnityEngine.EventSystems;

public class MessageContainer : MonoBehaviour, IPointerClickHandler
{
    [Header("Phone Manager")]
    [SerializeField] private PhoneManager m_phoneManager;
    [SerializeField] private float m_dragMinDist = 50.0f;
    private Vector2 m_startDragPos;
    
    public void OnPointerClick(PointerEventData pointerEventData)
    { 
        if (m_phoneManager.HasAlreadyClickedDp)
        { 
            m_phoneManager.CloseMenu();
        }
    }
}
