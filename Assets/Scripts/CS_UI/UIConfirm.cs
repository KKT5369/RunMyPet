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
    
    private Action okeyCallback;

    private void Awake()
    {
        SetAddListener();
    }

    public void Init(ConfirmData confirmData,Action callBack)
    {
        title.text = confirmData.title;
        body.text = confirmData.body;
        okeyCallback = callBack;
    }

    public void SetAddListener()
    {
        btnOkey.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            UIManager.Instance.CloseUI<UIConfirm>();
            okeyCallback.Invoke();
        }));
        btnCancel.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            UIManager.Instance.CloseUI<UIConfirm>();
        }));
    }

}
