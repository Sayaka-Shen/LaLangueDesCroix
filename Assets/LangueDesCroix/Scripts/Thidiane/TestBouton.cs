using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class TestBouton : MonoBehaviour
{
    [SerializeField] private GameObject note;
    [SerializeField] private GameObject message;
    [SerializeField] private GameObject task;
    [SerializeField] private RectTransform noteTarget;
    [SerializeField] private RectTransform messageTarget;
    [SerializeField] private RectTransform taskTarget;

    [SerializeField] private float WidthExpanded;
    [SerializeField] private float WidthCollapsed;


    void Start()
    {
        note.SetActive(false);
        message.SetActive(false);
        task.SetActive(false);
    }


    void Update()
    {
        
    }

    public void Spawnnote()
    {
        note.SetActive(true);
        message.SetActive(false);
        task.SetActive(false);
        noteTarget.sizeDelta = new Vector2(1000, 160);
        messageTarget.sizeDelta = new Vector2(160, 160);
        taskTarget.sizeDelta = new Vector2(160, 160);
    }
    
    public void Spawnmessage()
    {
        message.SetActive(true);
        note.SetActive(false);
        task.SetActive(false);
        messageTarget.sizeDelta = new Vector2(1000, 160);
        noteTarget.sizeDelta = new Vector2(160, 160);
        taskTarget.sizeDelta = new Vector2(160, 160);
    }

    public void Spawntask()
    {
        task.SetActive(true);
        note.SetActive(false);
        message.SetActive(false);
        taskTarget.sizeDelta = new Vector2(1000, 160);
        noteTarget.sizeDelta = new Vector2(160, 160);
        messageTarget.sizeDelta = new Vector2(160, 160);
    }
}
