using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupMenu : MonoBehaviour
{
    [SerializeField] private Button btn1;
    [SerializeField] private Button btn2;
    [SerializeField] private Button btn3;
    [SerializeField] private Button btnClose;

    [SerializeField] private TMP_Text btnTxt1;
    [SerializeField] private TMP_Text btnTxt2;
    [SerializeField] private TMP_Text btnTxt3;

    private Action _btn1Callback;
    private Action _btn2Callback;
    private Action _btn3Callback;
    
    void Start()
    {
        SetAddListener();
    }

    public void Init(ButtonPopupData data,[CanBeNull] Action btn1 ,[CanBeNull] Action btn2,[CanBeNull] Action btn3)
    {
        btnTxt1.text = data.btnText1;
        btnTxt2.text = data.btnText2;
        btnTxt3.text = data.btnText3;

        _btn1Callback = btn1;
        _btn2Callback = btn2;
        _btn3Callback = btn3;
    }

    void SetAddListener()
    {
        btn1.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            _btn1Callback?.Invoke();
        }));
        btn2.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            _btn2Callback?.Invoke();
        }));
        btn3.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            _btn3Callback?.Invoke();
        }));
        btnClose.onClick.AddListener((() => CloseUI()));
    }
    
    

    void CloseUI()
    {
        SoundManager.Instance.PlayUISound(SoundType.Button);
        Time.timeScale = 1;
        UIManager.Instance.CloseUI<UIPopupMenu>();
    }
}
