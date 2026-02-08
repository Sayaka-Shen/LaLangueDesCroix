using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;


public enum TypeBtn
{
    Message,
    Note,
    Galerie
}


public class BoutonChangeSprite : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TypeBtn typeBtn;

    [Header("Image Component")]
    [SerializeField] private Image imageComponentMessage;
    [SerializeField] private Image imageComponentNote;
    [SerializeField] private Image imageComponentGalerie;

    [Header("Sprite")]
    [SerializeField] private Sprite newSpriteBtnMessage;
    [SerializeField] private Sprite spriteBtnMessage;
    [SerializeField] private Sprite newSpriteBtnNote;
    [SerializeField] private Sprite spriteBtnNote;
    [SerializeField] private Sprite newSpriteBtnGalerie;
    [SerializeField] private Sprite spriteBtnGalerie;

    [Header("Background")]
    [SerializeField] private GameObject _bgMessage;
    [SerializeField] private GameObject _bgNote;
    [SerializeField] private GameObject _bgGalerie;

    [Header("Button")]
    [SerializeField] private GameObject _btnMessage;
    [SerializeField] private GameObject _btnNote;
    [SerializeField] private GameObject _btnGalerie;

    [SerializeField] private RectTransform _rectTransformMessage;
    [SerializeField] private RectTransform _rectTransformNote;
    [SerializeField] private RectTransform _rectTransformGalerie;

    void Start()
    {
        SwitchBtnbyTypeOnStart();
        _bgMessage.SetActive(false);
        _bgNote.SetActive(false);
        _bgGalerie.SetActive(false);
    }

    void SetNativeSize(RectTransform rt, Image img)
    {
        if (img.sprite == null) return;

        float w = img.sprite.rect.width;
        float h = img.sprite.rect.height;

        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
    }

    void RepositionButtons()
    {
        float spacing = 20f;
        float currentX = 0f;

        RectTransform[] buttons =
        {
        _rectTransformMessage,
        _rectTransformNote,
        _rectTransformGalerie
    };

        foreach (var rt in buttons)
        {
            rt.anchoredPosition = new Vector2(
                currentX + rt.rect.width / 2f,
                0
            );

            currentX += rt.rect.width + spacing;
        }

        float totalWidth = currentX - spacing;
        foreach (var rt in buttons)
        {
            rt.anchoredPosition -= new Vector2(totalWidth / 2f, 0);
        }
    }


    public void SwitchBtnbyTypeOnStart()
    {
        if(typeBtn == TypeBtn.Note)
        {
            spriteBtnNote = _btnNote.GetComponent<Image>().sprite;
            imageComponentNote.sprite = spriteBtnNote;
        }
        if (typeBtn == TypeBtn.Galerie)
        {
            spriteBtnGalerie = _btnGalerie.GetComponent<Image>().sprite;
            imageComponentGalerie.sprite = spriteBtnGalerie;
        }
        if (typeBtn == TypeBtn.Message)
        {
            spriteBtnMessage = _btnMessage.GetComponent<Image>().sprite;
            imageComponentMessage.sprite = spriteBtnMessage;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {

            if (typeBtn == TypeBtn.Note)
            {
                if (imageComponentNote.sprite == spriteBtnNote)
                {
                    switchBtnNote();
                }
                else
                {
                    imageComponentNote.sprite = spriteBtnNote;
                    _bgNote.SetActive(false);
                }
            }

            if (typeBtn == TypeBtn.Galerie)
            {
                if (imageComponentGalerie.sprite == spriteBtnGalerie)
                {
                    switchBtnGalerie();
                }
                else
                {
                    imageComponentGalerie.sprite = spriteBtnGalerie;
                    _bgGalerie.SetActive(false);
                }
            }

            if (typeBtn == TypeBtn.Message)
            {
                if (imageComponentMessage.sprite == spriteBtnMessage)
                {
                    switchBtnMessage();
                }
                else
                {
                    imageComponentMessage.sprite = spriteBtnMessage;
                    _bgMessage.SetActive(false);
                }
            }
    }

    public void switchBtnMessage()
    {
        imageComponentMessage.sprite = newSpriteBtnMessage;
        imageComponentNote.sprite = spriteBtnNote;
        imageComponentGalerie.sprite = spriteBtnGalerie;

        SetNativeSize(_rectTransformMessage, imageComponentMessage);
        SetNativeSize(_rectTransformNote, imageComponentNote);
        SetNativeSize(_rectTransformGalerie, imageComponentGalerie);

        RepositionButtons();

        _bgMessage.SetActive(true);
        _bgNote.SetActive(false);
        _bgGalerie.SetActive(false);
    }

    public void switchBtnNote()
    {
        imageComponentNote.sprite = newSpriteBtnNote;
        imageComponentGalerie.sprite = spriteBtnGalerie;
        imageComponentMessage.sprite = spriteBtnMessage;

        SetNativeSize(_rectTransformMessage, imageComponentMessage);
        SetNativeSize(_rectTransformNote, imageComponentNote);
        SetNativeSize(_rectTransformGalerie, imageComponentGalerie);

        RepositionButtons();

        _bgMessage.SetActive(false);
        _bgNote.SetActive(true);
        _bgGalerie.SetActive(false);
    }

    public void switchBtnGalerie()
    {
        imageComponentGalerie.sprite = newSpriteBtnGalerie;
        imageComponentNote.sprite = spriteBtnNote;   
        imageComponentMessage.sprite = spriteBtnMessage;

        SetNativeSize(_rectTransformMessage, imageComponentMessage);
        SetNativeSize(_rectTransformNote, imageComponentNote);
        SetNativeSize(_rectTransformGalerie, imageComponentGalerie);

        RepositionButtons();

        _bgMessage.SetActive(false);
        _bgNote.SetActive(false);
        _bgGalerie.SetActive(true);
    }
}
