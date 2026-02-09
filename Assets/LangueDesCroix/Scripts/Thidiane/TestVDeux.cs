using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum TypeBtnTest
{
    Message,
    Note,
    Galerie
}

public class TestVDeux : MonoBehaviour, IPointerClickHandler
{
    public TypeBtnTest typeBtn;

    public GameObject _bgMessage;
    public GameObject _bgNote;
    public GameObject _bgGalerie;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(typeBtn == TypeBtnTest.Message)
        {
            _bgMessage.SetActive(true);
            _bgNote.SetActive(false);
            _bgGalerie.SetActive(false);
            Debug.Log("Message");
        }
        if (typeBtn == TypeBtnTest.Note)
        {
            _bgNote.SetActive(true);
            _bgMessage.SetActive(false);
            _bgGalerie.SetActive(false);
            Debug.Log("Note");
        }
        if (typeBtn == TypeBtnTest.Galerie)
        {
            _bgGalerie.SetActive(true);
            _bgMessage.SetActive(false);
            _bgNote.SetActive(false);
            Debug.Log("Galerie");
        }
    }


}
