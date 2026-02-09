using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
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
    
    
    public void Initialize(ClickableImage newOrigin)
    {
        this.origin = newOrigin;

        imgTradImage.sprite = PrefabsManager.Instance.GetImage(origin.infos.image);
        imgSymbolOne.sprite = PrefabsManager.Instance.GetSymbol(origin.infos.symbolOne);
        imgSymbolTwo.sprite = PrefabsManager.Instance.GetSymbol(origin.infos.symbolTwo);

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
                sb1[origin.infos.indexRevealWordOne[i]] = sb2[origin.infos.indexRevealWordOne[i]];
                placeholderWordOne.text =  sb1.ToString();
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
                sb1[origin.infos.indexRevealWordTwo[i]] = sb2[origin.infos.indexRevealWordTwo[i]];
                placeholderWordTwo.text =  sb1.ToString();
            }
        }
    }

    public void onFirstValueChanged()
    {
        if (inptWordOne.text.Length > origin.infos.wordOne.Length)
        {
            inptWordOne.text = inptWordOne.text.Substring(0, origin.infos.wordOne.Length);
        }
        inptWordOne.text = inptWordOne.text.ToUpper();
    }
    
    public void onSecondValueChanged()
    {
        if (inptWordTwo.text.Length > origin.infos.wordTwo.Length)
        {
            inptWordTwo.text = inptWordTwo.text.Substring(0, origin.infos.wordTwo.Length);
        }
        
        inptWordTwo.text = inptWordTwo.text.ToUpper();
    }
    
    public void onFirstEndEdit()
    {
        String value = inptWordOne.text.ToUpper();
        if (value == origin.infos.wordOne)
        {
            Debug.Log("wordOne trouvé");
            origin.infos.foundWordOne = true;
            inptWordOne.interactable = false;
        }
        else
        {
            origin.infos.attemptsWordOne += 1;
            UpdateFirstPlaceHolder();
            inptWordOne.text = "";
            StartCoroutine(ChangeInputTextColor(inptWordOne));
        }
    }
    
    public void onSecondEndEdit()
    {
        String value = inptWordTwo.text.ToUpper();
        if (value == origin.infos.wordTwo)
        {
            Debug.Log("wordTwo trouvé");
            origin.infos.foundWordTwo = true;
            inptWordTwo.interactable = false;
        }
        else
        {
            origin.infos.attemptsWordTwo += 1;
            UpdateSecondPlaceHolder();
            inptWordTwo.text = "";
            StartCoroutine(ChangeInputTextColor(inptWordTwo));
        }
    }
    
    public void QuitTranslation()
    {
        origin.fillTwinInfos();
        Destroy(gameObject);
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
}
