using UnityEngine;
using System.Collections;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]private TypeBouton _currentType;

    [SerializeField] private GameObject _bgMessage;
    [SerializeField] private GameObject _bgNote;
    [SerializeField] private GameObject _bgGalerie;

    [Header("Message")]
    [SerializeField] private RectTransform _rectbgMessage;

    [Header("Galerie")]
    [SerializeField] private RectTransform _rectbgGalerie;

    [Header("Note")]
    [SerializeField] private RectTransform _rectbgNote;

    [Header("Animation")]
    public float _durationAnim = 0.5f;
    public float startPosXRight = 100;
    public float startPosXLeft = -100;
    public float EndPosX = 0;

    void Start()
    {

    }

    public void AfficherUI(TypeBouton type)
    {
        switch (type)
        {
            case TypeBouton.Message:

                _rectbgMessage.anchoredPosition = new Vector2(startPosXLeft, _rectbgMessage.anchoredPosition.y);

                _bgMessage.transform.SetSiblingIndex(2);
                _bgNote.transform.SetSiblingIndex(1);
                _bgGalerie.transform.SetSiblingIndex(0);

                _rectbgMessage.DOAnchorPosX(EndPosX, _durationAnim).SetEase(Ease.OutBack);
                break;
            case TypeBouton.Note:
                _rectbgNote.anchoredPosition = new Vector2(startPosXRight, _rectbgNote.anchoredPosition.y);

                _bgNote.transform.SetSiblingIndex(2);
                _bgMessage.transform.SetSiblingIndex(1);
                _bgGalerie.transform.SetSiblingIndex(0);

                _rectbgNote.DOAnchorPosX(EndPosX, _durationAnim).SetEase(Ease.OutBack);
                break;
            case TypeBouton.Galerie:

                if (_currentType == TypeBouton.Note)
                {
                    _rectbgGalerie.anchoredPosition = new Vector2(startPosXLeft, _rectbgGalerie.anchoredPosition.y);
                }

                if (_currentType == TypeBouton.Message)
                {
                    _rectbgGalerie.anchoredPosition = new Vector2(startPosXRight, _rectbgGalerie.anchoredPosition.y);
                }

                //_rectbgGalerie.anchoredPosition = new Vector2(startPosXRight, _rectbgGalerie.anchoredPosition.y);

                _bgGalerie.transform.SetSiblingIndex(2);
                _bgMessage.transform.SetSiblingIndex(1);
                _bgNote.transform.SetSiblingIndex(0);

                _rectbgGalerie.DOAnchorPosX(EndPosX, _durationAnim).SetEase(Ease.OutBack);

                break;
        }
        _currentType = type;
    }

   
}
