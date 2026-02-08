using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _bgMessage;
    [SerializeField] private GameObject _bgNote;
    [SerializeField] private GameObject _bgGalerie;

    void Start()
    {
        
    }

    public void AfficherUI(TypeBouton type)
    {
        switch (type)
        {
            case TypeBouton.Message:
                _bgMessage.SetActive(true);
                _bgNote.SetActive(false);
                _bgGalerie.SetActive(false);
                break;
            case TypeBouton.Note:
                _bgMessage.SetActive(false);
                _bgNote.SetActive(true);
                _bgGalerie.SetActive(false);
                break;
            case TypeBouton.Galerie:
                _bgMessage.SetActive(false);
                _bgNote.SetActive(false);
                _bgGalerie.SetActive(true);
                break;
        }
    }
}
