using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Testdéfilement : MonoBehaviour
{

    [Header("Transforms GameObject")]
    public RectTransform ScrollviewTransform;
    public RectTransform messageBtnTransform;
    public RectTransform questionChoiceTransform;
    //public RectTransform galerieNoteTransform;

    public GameObject a;
    public GameObject b;

    [Header("Spawn Positions Y")] // variable qui sert à changer la position des élément UI dans l'inspecteur
    public float messageYPos;
    public float questionYPos;
    public float scrollViewPosY;
    public float galerieNoteYPos;

    [Header("Despawn Positions Y")] // variable qui permet de les remettre à leur place d'origine (on fais comme on peut)
    //public float messageYPosb;
    //public float questionYPosb;
    //public float scrollViewPosYb;
    //public float galerieNoteYPosb;

    public float currentmessagePosY;
    public float currentquestionPosY;
    public float currentscrollViewPosY;
    public float currentgalerieNotePosY;

    void Start()
    {
        currentscrollViewPosY = ScrollviewTransform.anchoredPosition.y;
        currentmessagePosY = messageBtnTransform.anchoredPosition.y;
        currentquestionPosY = questionChoiceTransform.anchoredPosition.y;
        //currentgalerieNotePosY = galerieNoteTransform.anchoredPosition.y;

        a.SetActive(false);
        b.SetActive(false);
    }

    void Update()
    {
        
    }

    // Fonction à appeler dans un bouton pour faire apparaître et  défiler les menus
    public void spawnQuestionMenu()
    {
        ScrollviewTransform.DOAnchorPos(new Vector2(0, scrollViewPosY), 0.5f);
        messageBtnTransform.DOAnchorPos(new Vector2(0, messageYPos), 0.5f);
        questionChoiceTransform.DOAnchorPos(new Vector2(0, questionYPos), 0.5f);
        a.SetActive(true);
        b.SetActive(false);
    }

    public void SpawnGalerieNote()
    {
        ScrollviewTransform.DOAnchorPos(new Vector2(0, scrollViewPosY), 0.5f);
        messageBtnTransform.DOAnchorPos(new Vector2(0, messageYPos), 0.5f);
        b.SetActive(true);
        a.SetActive(false);
        //galerieNoteTransform.DOAnchorPos(new Vector2(0, galerieNoteYPos), 0.5f);
    }

    // Fonction à appeler dans un bouton pour faire disparaître et défiler les menus
    public void DespawnQuestionMenu()
    {
        ScrollviewTransform.DOAnchorPos(new Vector2(0, currentscrollViewPosY), 0.5f);
        messageBtnTransform.DOAnchorPos(new Vector2(0, currentmessagePosY), 0.5f);
        questionChoiceTransform.DOAnchorPos(new Vector2(0, currentquestionPosY), 0.5f);
        a.SetActive(false);
        b.SetActive(false);
    }
}
