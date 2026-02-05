using UnityEngine;
using UnityEngine.UI;

public class MessageImage : MonoBehaviour
{
    [Header("Image Element")]
    [SerializeField] private Image m_messageImg;

    public void SetSpriteImg(Sprite sprite)
    {
        m_messageImg.sprite = sprite;
    }
}
