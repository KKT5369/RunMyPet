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
    private Action<string> _okeyCallback;
    private Action _noCallback;
    private void Awake()
    {
        inputField.onEndEdit.AddListener((value) => _inputValue = value);
        Addlistener();
    }
    
    public void Init(string title,Action<string> okeyCallback = null , Action noCallback = null)
    {
        this.title.text = title;
        inputField.text = "";
        _okeyCallback = okeyCallback;
        _noCallback = noCallback;
    }

    public void Addlistener()
    {
        btnOkey.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            _okeyCallback.Invoke(_inputValue);
        }));
        
        btnCancel.onClick.AddListener((() =>
        {
            _noCallback?.Invoke();
            SoundManager.Instance.PlayUISound(SoundType.Button);
            UIManager.Instance.CloseUI<UIInputPopup>();
        }));
    } 

    
}
