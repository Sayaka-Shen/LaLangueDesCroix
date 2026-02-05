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
    
    [Header("Other Apps")]
    [SerializeField] private GameObject m_panelMessage;
    [SerializeField] private GameObject m_panelNotes;
    [SerializeField] private GameObject m_panelGallery;
    private AppState m_currentState = AppState.Message;
    private Dictionary<AppState, GameObject> m_getPanelFromAppState;

    public void Start()
    {
        m_messageContainerStartPos = m_messageContainer.anchoredPosition.y;

        m_getPanelFromAppState = new Dictionary<AppState, GameObject>()
        {
            { AppState.Message, m_panelMessage },
            { AppState.Gallery, m_panelGallery },
            { AppState.Notes, m_panelNotes },
        };
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

            if (ScrollContainer.transform.childCount > 2 && m_messageContainer.anchoredPosition.y == m_messageContainerStartPos)
            {
                Vector2 messageContainerCurrentPos = m_messageContainer.anchoredPosition;
                messageContainerCurrentPos.y += m_dropdownMenuFactor;
                m_messageContainer.DOAnchorPos(messageContainerCurrentPos, .5f);
            }
        }
    }

    public void CloseDropdownMenu()
    {
        HasAlreadyClickedDp = false;
        
        Vector2 dropDownContCurrentPos = m_dropdownContainer.anchoredPosition;
        dropDownContCurrentPos.y -= m_dropdownMenuFactor;
        m_dropdownContainer.DOAnchorPos(dropDownContCurrentPos, .5f);
            
        Vector2 dropDownFootCurrentPos = m_footerContainer.anchoredPosition;
        dropDownFootCurrentPos.y -= m_dropdownMenuFactor;
        m_footerContainer.DOAnchorPos(dropDownFootCurrentPos, .5f);

        if (ScrollContainer.transform.childCount > 2 && m_messageContainer.anchoredPosition.y != m_messageContainerStartPos)
        {
            Vector2 messageContainerCurrentPos = m_messageContainer.anchoredPosition;
            messageContainerCurrentPos.y -= m_dropdownMenuFactor;
            m_messageContainer.DOAnchorPos(messageContainerCurrentPos, .5f);
        }
    }

    public void OpenPhoneApplication(int appState)
    {
        if (m_currentState == (AppState)appState) return;
        
        m_getPanelFromAppState[m_currentState].transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        StartCoroutine(WaitBeforeHidingPanel(m_currentState));
        
        m_currentState = (AppState)appState;
        
        m_getPanelFromAppState[m_currentState].SetActive(true);
        m_getPanelFromAppState[m_currentState].transform.DOScale(new Vector3(1, 1, 1), 0.5f);
    }

    IEnumerator WaitBeforeHidingPanel(AppState appState)
    {
        yield return new WaitForSeconds(0.2f);
        m_getPanelFromAppState[appState].SetActive(false);
    }
}
