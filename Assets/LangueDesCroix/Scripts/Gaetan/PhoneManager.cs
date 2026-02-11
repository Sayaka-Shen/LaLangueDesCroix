using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public enum AppState
{
    Message = 0, 
    Gallery = 1, 
    Notes = 2,
}

public class PhoneManager : MonoBehaviour
{
    // Phone DropDown
    [Header("Dropdown Menu")] 
    [SerializeField] private RectTransform m_dropdownContainer;
    [SerializeField] private RectTransform m_footerContainer;
    [SerializeField] private RectTransform m_messageContainer;
    [SerializeField] private int m_dropdownMenuFactor = 100;
    [SerializeField] private GameObject m_scrollContainer;
    public GameObject ScrollContainer 
    { 
        get
        {
            return m_scrollContainer;
        }
    }

    public bool HasAlreadyClickedDp { get; private set; }
    private float m_messageContainerStartPos;
    private RectTransform m_scrollContRectTransform;

    [Header("Popup")]
    [SerializeField] private RectTransform m_popupTransform;
    public bool HasPopupOpened { get; private set; }
    


    public void Start()
    {
        m_scrollContRectTransform = m_scrollContainer.GetComponent<RectTransform>();
        m_messageContainerStartPos = m_messageContainer.anchoredPosition.y;
    }

    public void OpenDropDownMenu()
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

            if (m_scrollContRectTransform.sizeDelta.y > 0 && m_messageContainer.anchoredPosition.y == m_messageContainerStartPos)
            {
                Vector2 messageContainerCurrentPos = m_messageContainer.anchoredPosition;
                messageContainerCurrentPos.y += m_dropdownMenuFactor;
                m_messageContainer.DOAnchorPos(messageContainerCurrentPos, .5f);
            }
        }
    }

    public void CloseDropdownMenu()
    {
        if (HasAlreadyClickedDp)
        {
            HasAlreadyClickedDp = false;

            Vector2 dropDownContCurrentPos = m_dropdownContainer.anchoredPosition;
            dropDownContCurrentPos.y -= m_dropdownMenuFactor;
            m_dropdownContainer.DOAnchorPos(dropDownContCurrentPos, .5f);

            Vector2 dropDownFootCurrentPos = m_footerContainer.anchoredPosition;
            dropDownFootCurrentPos.y -= m_dropdownMenuFactor;
            m_footerContainer.DOAnchorPos(dropDownFootCurrentPos, .5f);

            if (m_scrollContRectTransform.sizeDelta.y > 0 && m_messageContainer.anchoredPosition.y != m_messageContainerStartPos)
            {
                Vector2 messageContainerCurrentPos = m_messageContainer.anchoredPosition;
                messageContainerCurrentPos.y -= m_dropdownMenuFactor;
                m_messageContainer.DOAnchorPos(messageContainerCurrentPos, .5f);
            }
        }
    }
    public void OpenPopup()
    {
        if (!HasPopupOpened)
        {
            HasPopupOpened = true;
            m_popupTransform.DOAnchorPosY(-40, 1.0f);

            StartCoroutine(WaitBeforeClosingPopup());
        }
    }

    public void ClosePopup()
    {
        if (HasPopupOpened)
        {
            HasPopupOpened = false;
            m_popupTransform.DOAnchorPosY(180, 1.0f);
        }
    }

    public IEnumerator WaitBeforeClosingPopup()
    {
        yield return new WaitForSeconds(4.0f);
        ClosePopup();
    }
}
