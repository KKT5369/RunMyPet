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
    
    void Start()
    {
        btnClose.onClick.AddListener((() => CloseUI()));
    }

    public void AddListener(ButtonPopupData data,[CanBeNull] Action btn1 ,[CanBeNull] Action btn2,[CanBeNull] Action btn3)
    {
        btnTxt1.text = data.btnText1;
        btnTxt2.text = data.btnText2;
        btnTxt3.text = data.btnText3;

        this.btn1.onClick.AddListener((() => btn1?.Invoke()));
        this.btn2.onClick.AddListener((() => btn2?.Invoke()));
        this.btn3.onClick.AddListener((() => btn3?.Invoke()));
    }

    void CloseUI()
    {
        Time.timeScale = 1;
        UIManager.Instance.CloseUI<UIPopupMenu>();
    }
}
