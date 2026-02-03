using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelocateScrollView : MonoBehaviour
{
    private RectTransform m_rectTransform;
    private GameObject[] m_scrollChildren;
    
    private void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(m_rectTransform);
        UpdateScrollView();
    }
    
    public void UpdateScrollView()
    {
        float currentHeightY = m_rectTransform.sizeDelta.y;
        m_rectTransform.anchoredPosition = new Vector2(m_rectTransform.anchoredPosition.x, currentHeightY);
    }
}
