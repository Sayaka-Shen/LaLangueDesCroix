using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChoiceController : MonoBehaviour
{
    [Header("Button Properties")]
    [SerializeField] private TextMeshProUGUI m_btnText;
    
    //private bool lockState = false;
    
    //private Button _button;
    //private Animator _animator;

    private void Awake()
    {
        //_animator = GetComponent<Animator>();
        //_button = GetComponent<Button>();
    }

    public void InitializeButtonChoiceController(bool fillCondition, string text)
    {
        m_btnText.SetText(text);

        //lockState = !fillCondition;
        //if(_button == null) _button = GetComponent<Button>();
        //_button.interactable = !lockState;

        //if(_animator == null) _animator = GetComponent<Animator>();
        //_animator.SetBool("Locked", lockState);
    }

    public void OnClicked()
    {
        //_animator.SetTrigger("Clicked");

        //if (lockState)
        //{
        //    return;
        //}
    }

}
