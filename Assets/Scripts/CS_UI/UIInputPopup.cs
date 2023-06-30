using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInputPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button btnOkey;
    [SerializeField] private Button btnCancel;

    private string _inputValue;
    private Action<string> okeyCallback;
    private Action noCallback;
    private void Awake()
    {
        inputField.onEndEdit.AddListener((value) => _inputValue = value);
        Addlistener();
    }
    
    public void Setting(string title,Action<string> okeyCallback = null , Action noCallback = null)
    {
        this.title.text = title;
        inputField.text = "";
        this.okeyCallback = okeyCallback;
        this.noCallback = noCallback;
    }

    public void Addlistener()
    {
        btnOkey.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            okeyCallback.Invoke(_inputValue);
        }));
        
        btnCancel.onClick.AddListener((() =>
        {
            noCallback?.Invoke();
            SoundManager.Instance.PlayUISound(SoundType.Button);
            UIManager.Instance.CloseUI<UIInputPopup>();
        }));
    } 

    
}
