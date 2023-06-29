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
    

    public void Init(ConfirmData confirmData,Action okeyCallback = null,Action noCallback = null)
    {
        title.text = confirmData.title;
        body.text = confirmData.body;
        btnOkey.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            UIManager.Instance.CloseUI<UIConfirm>();
            okeyCallback?.Invoke();
        }));
        btnCancel.onClick.AddListener((() =>
        {
            noCallback?.Invoke();
            SoundManager.Instance.PlayUISound(SoundType.Button);
            UIManager.Instance.CloseUI<UIConfirm>();
        }));
    }

}
