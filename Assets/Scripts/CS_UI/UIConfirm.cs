using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIConfirm : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text body;
    [SerializeField] private Button btnOkey;
    [SerializeField] private Button btnCancel;

    private Action _okeyCallback;
    private Action _noCallback;

    private void Awake()
    {
        SetAddlistener();
    }

    public void Init(ConfirmData confirmData,Action okeyCallback = null,Action noCallback = null)
    {
        title.text = confirmData.title;
        body.text = confirmData.body;
        _okeyCallback = okeyCallback;
        _noCallback = noCallback;
    }

    void SetAddlistener()
    {
        btnOkey.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            UIManager.Instance.CloseUI<UIConfirm>();
            _okeyCallback?.Invoke();
        }));
        btnCancel.onClick.AddListener((() =>
        {
            _noCallback?.Invoke();
            SoundManager.Instance.PlayUISound(SoundType.Button);
            UIManager.Instance.CloseUI<UIConfirm>();
        }));
    }

}
