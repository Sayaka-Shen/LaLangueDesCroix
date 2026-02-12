using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Translation : MonoBehaviour
{
    private ClickableImage origin;
    
    [SerializeField] private Image imgTradImage;
    [SerializeField] private Image imgSymbolOne;
    [SerializeField] private Image imgSymbolTwo;
    [SerializeField] private TMP_InputField inptWordOne;
    [SerializeField] private TMP_InputField inptWordTwo;
    [SerializeField] private TextMeshProUGUI  placeholderWordOne;
    [SerializeField] private TextMeshProUGUI  placeholderWordTwo;
    [SerializeField] private GameObject  goInput;

    public void Initialize(ClickableImage newOrigin)
    {
        this.origin = newOrigin;

        imgTradImage.sprite = PrefabsManager.Instance.GetImage(origin.infos.image);
        imgSymbolOne.sprite = PrefabsManager.Instance.GetSymbol(origin.infos.symbolOne);
        if (origin.infos.symbolTwo == SYMBOLS.Null)
        {
            imgSymbolTwo.transform.parent.gameObject.SetActive(false);
            origin.infos.foundWordTwo = true;
        }
        else
        {
            imgSymbolTwo.sprite = PrefabsManager.Instance.GetSymbol(origin.infos.symbolTwo);
        }
        

        InitializePlaceholders();
        CheckFields();
    }

    public void CheckFields()
    {
        if (origin.infos.foundWordOne)
        {
            placeholderWordOne.text = origin.infos.wordOne;
            inptWordOne.interactable = false;
        }
        
        if (origin.infos.foundWordTwo)
        {
            placeholderWordTwo.text = origin.infos.wordTwo;
            inptWordTwo.interactable = false;
        }
    }
    
    public void InitializePlaceholders()
    {
        String first = "";
        String second = "";
        
        for (int i = 0; i < origin.infos.wordOne.Length; i++)
        {
            first += "_";
        }
        
        for (int i = 0; i < origin.infos.wordTwo.Length; i++)
        {
            second += "_";
        }

        placeholderWordOne.text = first;
        placeholderWordTwo.text = second;
        UpdatePlaceHolders();
    }

    public void UpdatePlaceHolders()
    {
        UpdateFirstPlaceHolder();
        UpdateSecondPlaceHolder();
    }

    private void UpdateFirstPlaceHolder()
    {
        for (int i = 0; i < origin.infos.attemptsRevealWordOne.Length; i++)
        {
            if (origin.infos.attemptsWordOne >= origin.infos.attemptsRevealWordOne[i])
            {
                var sb1 = new StringBuilder(placeholderWordOne.text);
                var sb2 = new StringBuilder(origin.infos.wordOne);
                if (origin.infos.indexRevealWordOne[i] >= 0 & origin.infos.indexRevealWordOne[i] < origin.infos.wordOne.Length)
                {
                    sb1[origin.infos.indexRevealWordOne[i]] = sb2[origin.infos.indexRevealWordOne[i]];
                    placeholderWordOne.text =  sb1.ToString();
                }
            }
        }
    }
    
    private void UpdateSecondPlaceHolder()
    {
        for (int i = 0; i < origin.infos.attemptsRevealWordTwo.Length; i++)
        {
            if (origin.infos.attemptsWordTwo >= origin.infos.attemptsRevealWordTwo[i])
            {
                var sb1 = new StringBuilder(placeholderWordTwo.text);
                var sb2 = new StringBuilder(origin.infos.wordTwo);
                if (origin.infos.indexRevealWordTwo[i] >= 0 & origin.infos.indexRevealWordTwo[i] < origin.infos.wordTwo.Length)
                {
                    sb1[origin.infos.indexRevealWordTwo[i]] = sb2[origin.infos.indexRevealWordTwo[i]];
                    placeholderWordTwo.text =  sb1.ToString();
                }
                
            }
        }
    }

    public void onInputSelected()
    {
        EnableInput();
    }

    public void onFirstValueChanged()
    {
        if (inptWordOne.text.Length > origin.infos.wordOne.Length)
        {
            inptWordOne.text = inptWordOne.text.Substring(0, origin.infos.wordOne.Length);
        }
        inptWordOne.text = inptWordOne.text.ToUpper();
        UpdateInput(inptWordOne.text);
    }
    
    public void onSecondValueChanged()
    {
        if (inptWordTwo.text.Length > origin.infos.wordTwo.Length)
        {
            inptWordTwo.text = inptWordTwo.text.Substring(0, origin.infos.wordTwo.Length);
        }
        inptWordTwo.text = inptWordTwo.text.ToUpper();
        UpdateInput(inptWordTwo.text);
    }
    
    public void onFirstEndEdit()
    {
        String value = inptWordOne.text.ToUpper();
        if (value == origin.infos.wordOne)
        {
            Debug.Log("wordOne trouvé");
            origin.infos.foundWordOne = true;
            inptWordOne.interactable = false;
            AudioManager.instance.PlaySFX("translation_right");
            CheckBothValidated();
        }
        else
        {
            AudioManager.instance.PlaySFX("translation_wrong");
            origin.infos.attemptsWordOne += 1;
            UpdateFirstPlaceHolder();
            inptWordOne.text = "";
            if (!EventSystem.current.alreadySelecting) EventSystem.current.SetSelectedGameObject(null);
            StartCoroutine(ChangeInputTextColor(inptWordOne));
        }
        
        DisableInput();
    }
    
    public void onSecondEndEdit()
    {
        String value = inptWordTwo.text.ToUpper();
        if (value == origin.infos.wordTwo)
        {
            Debug.Log("wordTwo trouvé");
            origin.infos.foundWordTwo = true;
            inptWordTwo.interactable = false;
            AudioManager.instance.PlaySFX("translation_right");
            CheckBothValidated();
        }
        else
        {
            AudioManager.instance.PlaySFX("translation_wrong");
            origin.infos.attemptsWordTwo += 1;
            UpdateSecondPlaceHolder();
            inptWordTwo.text = "";
            if (!EventSystem.current.alreadySelecting) EventSystem.current.SetSelectedGameObject(null);
            StartCoroutine(ChangeInputTextColor(inptWordTwo));
        }
        
        DisableInput();
    }

    public void CheckBothValidated()
    {
        if (origin.infos.foundWordOne && origin.infos.foundWordTwo)
        {
            origin.infos.parentMessage.transform.parent.GetChild(
                origin.infos.parentMessage.transform.GetSiblingIndex() - origin.infos.messageIndexToReplace)
                .GetComponentInChildren<Message>().SetMessageText(origin.infos.messageDecrypted);

            InstantiateNotes();

            StartCoroutine(WaitBeforeQuittingTranslation());
        }
    }

    public void InstantiateNotes()
    {
        Transform parent = PrefabsManager.Instance.goNoteContainer.transform;

        GameObject symbol = Instantiate(PrefabsManager.Instance.prefabNoteSymbol, parent.GetChild(0).transform);
        if (origin.infos.symbolTwo != SYMBOLS.Null)
        {
            GameObject symbolTwo = Instantiate(PrefabsManager.Instance.prefabNoteSymbol, parent.GetChild(0).transform);
            NoteSymbol noteSymbolTwo = symbolTwo.GetComponent<NoteSymbol>();

            if (noteSymbolTwo != null)
            {
                noteSymbolTwo.Initialize(PrefabsManager.Instance.GetSymbol(origin.infos.symbolTwo), origin.infos.wordTwo);
            }
        }
        
        GameObject sentence =Instantiate(PrefabsManager.Instance.prefabNoteSentence, parent.GetChild(1).transform);
        NoteSymbol noteSymbol = symbol.GetComponent<NoteSymbol>();
        
        NoteSentence noteSentence = sentence.GetComponent<NoteSentence>();

        if (noteSymbol != null && noteSentence != null)
        {
            noteSymbol.Initialize(PrefabsManager.Instance.GetSymbol(origin.infos.symbolOne), origin.infos.wordOne);
            
            String symbols = "";
            symbols += PrefabsManager.Instance.GetStringFromSymbol(origin.infos.symbolOne);
            symbols += PrefabsManager.Instance.GetStringFromSymbol(origin.infos.symbolTwo);
            noteSentence.Initialize(symbols, origin.infos.messageDecrypted);
        }
    }
    
    public void QuitTranslation()
    {
        origin.fillTwinInfos();
        Destroy(gameObject);
    }

    public IEnumerator WaitBeforeQuittingTranslation()
    {
        yield return new WaitForSeconds(.5f);
        QuitTranslation();
        PrefabsManager.Instance.dialogueManager.IsWaitingForTraduction = false;
    }

    IEnumerator ChangeInputTextColor(TMP_InputField inputField)
    {
        if (inputField.TryGetComponent<Image>(out Image bg))
        {
            bg.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            bg.color = Color.white;
        }
    }

    
    //Quelques fonctions externes pour simplie le code psk j'en ai marre
    //(oui pas forcément utile mais on est juste après le cémantix "crédible" ok)
    private void EnableInput()
    {
        goInput.SetActive(true);
    }

    private void DisableInput()
    {
        ClearInput();
        goInput.SetActive(false);
    }

    private void UpdateInput(String newString)
    {
        TextMeshProUGUI inputText = goInput.GetComponentInChildren<TextMeshProUGUI>();
        if (inputText != null) inputText.text = newString;
    }

    private void ClearInput()
    {
        TextMeshProUGUI inputText = goInput.GetComponentInChildren<TextMeshProUGUI>();
        if (inputText != null) inputText.text = "";
    }
}
