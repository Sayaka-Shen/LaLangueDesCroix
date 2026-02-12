using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;

public enum language
{
    FR,
    EN,
}

//public enum bubleType
//{
//    NORMAL,
//    THINK,
//    SHOUT,
//}

public class DialogueManager : MonoBehaviour
{
    public DSGraphSaveDataSO runtimeGraph;
    
    [Header("PLAYER SETTINGS")]
    [SerializeField] private language languageSetting = language.FR;
    private language _previewLanguage;

    [Header("UI Elements")]
    [SerializeField] private dialogueContainer m_dialogueContainer;
    //private Dictionary<bubleType, DialogueContainer> _bubleContainers = new Dictionary<bubleType, DialogueContainer>();
    //[SerializeField] private List<DialogueContainer> _bubleContainerList = new List<DialogueContainer>();

    [Header("Choice Button UI")]
    public Button ChoiceButtonPrefab;
    public Transform ChoiceButtonContainer;
    public Transform m_phoneChoiceBtn;

    public Speakers SpeakersScriptable;
    private SpeakerInfo _currentSpeaker;

    private Dictionary<string, DSNodeSaveData> _nodeLookup = new Dictionary<string, DSNodeSaveData>();
    private DSNodeSaveData _currentNode;
    public bool IsWaitingForChoice { get; private set; }
    
    private dialogueContainer m_currentDialogueContainer;
    //private dialogueContainer _oldDialogueContainer;

    [Header("Phone Manager")]
    [SerializeField] private PhoneManager m_phoneManager;

    [Header("Message Timer")]
    [SerializeField] private float m_maxTimer = 1.5f;
    private float m_timer;
    private bool m_isWaitingForMessage;

    [Header("Shake Timer")]
    [SerializeField] private float m_maxShakeTimer = 2.0f;
    [SerializeField] private float m_shakeForce = 2.0f;
    [SerializeField] private float m_shakeTime = 1.0f;
    private float m_shakeTimer;
    private bool m_isWaitingToShakeAgain;

    [Header("Traduction")]
    public bool IsWaitingForTraduction { get; set; }


    [Button]
    public void LoadCsv()
    {
        FantasyDialogueTable.Load();
    }

    #if UNITY_EDITOR
    // POUR METTRE A JOUR LE DIALOGUE ET LES TEXTES SI UNE NOUVELLE LANGUE EST CHOISIE //
    private void OnValidate()
    {
        if (_previewLanguage != languageSetting)
        {
            _previewLanguage = languageSetting;
            UpdateLanguageSetting(languageSetting);
        }
    }
    #endif
    
    
    // MET A JOUR LA LANGUE DU DIALOGUE //
    public void UpdateLanguageSetting(language newLanguage)
    {
        languageSetting = newLanguage;
        if (IsWaitingForChoice)
        {
            foreach (Transform child in ChoiceButtonContainer)
            {
                Destroy(child.gameObject);
            }
        }
        UpdateDialogueFromNode(_currentNode);
    }
    
    private void Awake()
    {
        // ON FAIT CA EN BRUT PRCQ NSM PAS LE TEMPS // Courage pour le projet UNITY MOBILE je vous aime tous <3 //
        
        //_bubleContainers.Add(bubleType.NORMAL, _bubleContainerList[0]);
        //_bubleContainers.Add(bubleType.THINK, _bubleContainerList[1]);
        //_bubleContainers.Add(bubleType.SHOUT, _bubleContainerList[2]);
    }

    private void Start()
    {
        IsWaitingForChoice = false;

        _previewLanguage = languageSetting;
        FantasyDialogueTable.Load();
        foreach (var node in runtimeGraph.Nodes)
        {
            _nodeLookup[node.ID] = node;
        }

        // GET NODE LIFE CYCLE
        _currentNode = GetNodeStart();
        //////////////////////
        
        if (_currentNode == null)
        {
            EndDialogue();
            return;
        }
        else
        {
            TryToUpdateNextDialogueFromNextNode();
        }
    }
    
    private DSNodeSaveData GetNextNode(string nextID)
    {
        if (!string.IsNullOrEmpty(nextID) && _nodeLookup.TryGetValue(nextID, out var node))
        {
            return node;
        }
        return null;
    }

    private void Update()
    {
        // Shake
        if(IsWaitingForChoice && !m_isWaitingToShakeAgain)
        {
            m_shakeTimer += Time.deltaTime;
        }

        // Shake Timer finished
        if(m_shakeTimer >= m_maxShakeTimer)
        {
            m_isWaitingToShakeAgain = true;
        }

        if (IsWaitingForChoice && m_isWaitingToShakeAgain && !m_phoneManager.HasAlreadyClickedDp)
        {
            m_phoneChoiceBtn.DOShakePosition(m_shakeTime, m_shakeForce);

            // Reset Shake timer
            m_shakeTimer = 0.0f;
            m_isWaitingToShakeAgain = false;
        }

        // Base Loop
        if (!IsWaitingForChoice)
        {
            m_timer += Time.deltaTime;
        }
       
        if (m_timer >= m_maxTimer)
        {
            m_isWaitingForMessage = true;
        }

        if (m_isWaitingForMessage && !IsWaitingForChoice && !IsWaitingForTraduction)
        {
            m_isWaitingForMessage = false;
            m_timer = 0; 

            TryToUpdateNextDialogueFromNextNode();
        }
    }

    // MET A JOUR LE DIALOGUE EN FONCTION DU NODE SUIVANT //
    private void TryToUpdateNextDialogueFromNextNode()
    {
        // CHECK SI NEXT NODE EXISTE //
        if (_currentNode == null)
        {
            return;
        }

        DSNodeSaveData nextNode = new DSNodeSaveData();
        
        switch (_currentNode.DialogueType) // QUEL TYPE DE NODE ON EST ACUTELLEMENT //
        {
            case DSDialogueType.Start:
                nextNode = GetNextNode(_currentNode.ChoicesInNode[0].NodeID);
                break;
            case DSDialogueType.End:
                EndDialogue();
                return;
            case DSDialogueType.Branch:
                nextNode = GetCorrectNextNodeFromBranch();
                break;
            case DSDialogueType.MultipleChoice:
                nextNode = GetNextNode(_currentNode.ChoicesInNode[0].NodeID);
                break;
        }
        
        if (nextNode == null)
        {
            Debug.Log("Next Node is null. Ending dialogue.");
            EndDialogue();
            return;
        }

        UpdateDialogueFromNode(nextNode);
    }
    
    // GERE LES CONDITIONS DU BRANCH // RETOURNE LE BON NODE EN FONCTION DES CONDITIONS //s
    private DSNodeSaveData GetCorrectNextNodeFromBranch()
    {
        Debug.Log("Evaluating branch conditions for Node ID: " + _currentNode.ID);
        // SI Y'A PAS DE CONDITIONS DANS UN IF ON SKIP // NORMALEMENT CA DEVRAIT JAMAIS ARRIVER MDR//
        if(_currentNode.ChoicesInNode[0].Conditions.Count <= 0)
        {
            Debug.Log("No choices available in the current branch node.");
            return GetNextNode(_currentNode.ChoicesInNode[1].NodeID);
        }
        
        bool hasMetConditions = false;

        foreach (var choice in _currentNode.ChoicesInNode[0].Conditions)
        {
            if (_currentNode.OnlyOneConditionNeeded)
            {
                if (DoesFillCondtions(choice))
                {
                    hasMetConditions = true;
                    // ON RECUP LE [1] CAR C'EST LE TRUE //
                    break;
                }
                continue;
            }
            
            if (!DoesFillCondtions(choice))
            {
                hasMetConditions = false;
                break;
            }

            hasMetConditions = true;
        }

        if (hasMetConditions)
        {
            // ON RECUP LE [1] CAR C'EST LE TRUE //
            return GetNextNode(_currentNode.ChoicesInNode[1].NodeID);
        }
        // ON RECUP LE [2] CAR C'EST LE FALSE //
        return GetNextNode(_currentNode.ChoicesInNode[2].NodeID);
    }

    // MET A JOUR LE DIALOGUE EN FONCTION DU NODE DONNE //
    private void UpdateDialogueFromNode(DSNodeSaveData node)
    {
        _currentNode = node;

        if (_currentNode == null)
        {
            EndDialogue();
            return;
        }
        
        switch (_currentNode.DialogueType) // QUEL TYPE DE NODE ON EST ACUTELLEMENT //
        {
            case DSDialogueType.Start:
                // Normalement on devrait jamais y arriver mdrr // Mais au cas où //
                Debug.Log("Current Node is of type Start, moving to next node.");
                UpdateDialogueFromNode(GetNextNode(_currentNode.ChoicesInNode[0].NodeID));
                break;
            case DSDialogueType.End:
                EndDialogue();
                return;
            case DSDialogueType.Branch:
                TryToUpdateNextDialogueFromNextNode();
                break;
            case DSDialogueType.MultipleChoice:
                CreateButtonsChoice();
                break;
        }

        //if (_bubleContainers.TryGetValue(_currentNode.GetBubleType(), out var container))
        //{
        //    _currentDialogueContainer = container;
        //}
        //else
        //{
        //    return;
        //}

        m_currentDialogueContainer = m_dialogueContainer;
        if (m_currentDialogueContainer == null)
        {
            Debug.Log("Current Dialogue Container is null.");
            return;
        }
        
        //if(_oldDialogueContainer != null)
        //{
        //    //_oldDialogueContainer.HideContainer();
        //}

        //_oldDialogueContainer = m_currentDialogueContainer;

        ChangeSpeaker(_currentNode.Speaker);

        if(_currentSpeaker == null)
        {
            Debug.Log("Current Speaker is null.");
            return;
        }
        
        string targetDialogue = FantasyDialogueTable.LocalManager.FindDialogue(_currentNode.GetDropDownKeyDialogue(), Enum.GetName(typeof(language), languageSetting));
        m_currentDialogueContainer.InitializeDialogueContainer(targetDialogue, _currentNode.TraductionImage, _currentSpeaker.speakEnum, _currentNode.PopupText /*_currentNode.TraductionImage.sprite*/);
    }

    private void CreateButtonsChoice()
    {
        if (_currentNode.ChoicesInNode.Count >= 1 && _currentNode.isMultipleChoice)
        {
            IsWaitingForChoice = true;

            foreach (DSChoiceSaveData choice in _currentNode.ChoicesInNode)
            {
                Button choiceButton = Instantiate(ChoiceButtonPrefab, ChoiceButtonContainer);
                ButtonChoiceController buttonController = choiceButton.GetComponent<ButtonChoiceController>();
                if (buttonController != null)
                {
                    bool fillCondition = true;

                    foreach (var condition in choice.Conditions)
                    {
                        fillCondition = DoesFillCondtions(condition);
                        if (!fillCondition)
                        {
                            break;
                        }
                    }

                    string textButton = FantasyDialogueTable.LocalManager.FindDialogue(choice.GetDropDownKeyChoice(), Enum.GetName(typeof(language), languageSetting));
                    buttonController.InitializeButtonChoiceController(fillCondition, textButton);
                }
                
                choiceButton.onClick.AddListener(() =>
                {
                    IsWaitingForChoice = false;
                    UpdateDialogueFromNode(GetNextNode(choice.NodeID));
                    m_phoneManager.CloseDropdownMenu();

                    foreach (Transform child in ChoiceButtonContainer)
                    {
                        if (child == null) continue;
                        Destroy(child.gameObject);
                    }
                });
            }
        }
    }
    
    private bool DoesFillCondtions(ConditionsSC choice)
    {
        return PlayerInventoryManager.instance.DoesPlayerFillCondition(choice);
    }

    private void EndDialogue()
    {
        //_currentDialogueContainer.HideContainer();
        _currentNode = null;

        foreach (Transform child in ChoiceButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void ChangeSpeaker(Espeaker speak)
    {
        foreach (var speaker in SpeakersScriptable.speakers)
        {
            if (speaker.speakEnum == speak)
            {
                SetNewSpeaker(speaker);
            }
        }
    }
    
    private void SetNewSpeaker(SpeakerInfo speaker)
    {
        _currentSpeaker = speaker;
    }

    private DSNodeSaveData GetNodeStart()
    {
        foreach (var node in runtimeGraph.Nodes)
        {
            if (node.DialogueType == DSDialogueType.Start)
            {
                return node;
            }
        }
        return null;
    }
}