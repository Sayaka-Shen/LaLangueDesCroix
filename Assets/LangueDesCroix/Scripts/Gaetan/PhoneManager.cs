using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour
{
    // Phone DropDown
    [Header("Dropdown Menu")] 
    [SerializeField] private RectTransform m_dropdownContainer;
    [SerializeField] private RectTransform m_footerContainer;
    [SerializeField] private RectTransform m_messageContainer;
    [SerializeField] private int m_dropdownMenuFactor = 100;
    [SerializeField] private GameObject m_choiceSubContainer;
    [SerializeField] private GameObject m_appSubContainer;
    public bool HasAlreadyClickedDp { get; private set; }
    
    public void OpenDropDownMenu(bool isAppBtn = false)
    {
        if (!HasAlreadyClickedDp)
        {
            HasAlreadyClickedDp = true;

            Vector2 dropDownContCurrentPos = m_dropdownContainer.anchoredPosition;
            dropDownContCurrentPos.y += m_dropdownMenuFactor;
            m_dropdownContainer.DOAnchorPos(dropDownContCurrentPos, .5f);
    
            Vector2 dropDownFootCurrentPos = m_footerContainer.anchoredPosition;
            dropDownFootCurrentPos.y += m_dropdownMenuFactor;
            m_footerContainer.DOAnchorPos(dropDownFootCurrentPos, .5f);
    
            Vector2 messageContainerCurrentPos = m_messageContainer.anchoredPosition;
            messageContainerCurrentPos.y += m_dropdownMenuFactor;
            m_messageContainer.DOAnchorPos(messageContainerCurrentPos, .5f); 
        }
        
        if (isAppBtn)
        {
            m_appSubContainer.SetActive(true);
            m_choiceSubContainer.SetActive(false);
        }
        else
        {
            m_choiceSubContainer.SetActive(true);
            m_appSubContainer.SetActive(false);
        }
    }

    public void CloseMenu()
    {
        HasAlreadyClickedDp = false;
        
        Vector2 dropDownContCurrentPos = m_dropdownContainer.anchoredPosition;
        dropDownContCurrentPos.y -= m_dropdownMenuFactor;
        m_dropdownContainer.DOAnchorPos(dropDownContCurrentPos, .5f);
            
        Vector2 dropDownFootCurrentPos = m_footerContainer.anchoredPosition;
        dropDownFootCurrentPos.y -= m_dropdownMenuFactor;
        m_footerContainer.DOAnchorPos(dropDownFootCurrentPos, .5f);
            
        Vector2 messageContainerCurrentPos = m_messageContainer.anchoredPosition;
        messageContainerCurrentPos.y -= m_dropdownMenuFactor;
        m_messageContainer.DOAnchorPos(messageContainerCurrentPos, .5f);
            
        m_choiceSubContainer.SetActive(false);
        m_appSubContainer.SetActive(false);
    }
}
