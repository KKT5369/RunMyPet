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

    private void Awake()
    {
        inputField.onEndEdit.AddListener((value) => _inputValue = value);
    }

    public void Addlistener(string title,Action<string> callback)
    {
        btnOkey.onClick.AddListener((() =>
        {
            this.title.text = title;
            callback.Invoke(_inputValue);
        }));
        
        btnCancel.onClick.AddListener((() => UIManager.Instance.CloseUI<UIInputPopup>()));
    }
}
