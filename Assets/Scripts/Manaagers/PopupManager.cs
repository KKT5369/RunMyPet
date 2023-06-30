using System;
using JetBrains.Annotations;
using UnityEngine;

public class PopupManager : SingleTon<PopupManager>
{
    private UIConfirm _UIconfirm;
    private UIPopupMenu _uiButtonPopup;
    private UIInputPopup _uiInputPopup;

    /// <summary>
    /// data >> 타이틀과 내용 정보
    /// </summary>
    /// <param name="data"></param>
    /// <param name="callback"></param>
    public void ConfirmPopup(ConfirmData data, Action OkeyCallback = null,Action noCallback = null)
    {
        if (_UIconfirm == null)
        {
            UIManager.Instance.OpenUI<UIConfirm>();
            _UIconfirm = UIManager.Instance.GetUI<UIConfirm>().GetComponent<UIConfirm>();
        }
        UIManager.Instance.OpenUI<UIConfirm>();
        _UIconfirm.Init(data,OkeyCallback,noCallback);
    }
    
    /// <summary>
    /// data >> 버튼 텍스트 정보
    /// btn >> 버튼별 콜백함수
    /// </summary>
    /// <param name="data"></param>
    /// <param name="btn1"></param>
    /// <param name="btn2"></param>
    /// <param name="btn3"></param>
    public void ButtonPopup(ButtonPopupData data,[CanBeNull] Action btn1,[CanBeNull] Action btn2,[CanBeNull] Action btn3)
    {
        if (_uiButtonPopup == null) 
        {
            UIManager.Instance.OpenUI<UIPopupMenu>();
            _uiButtonPopup = UIManager.Instance.GetUI<UIPopupMenu>().GetComponent<UIPopupMenu>();
        }
        UIManager.Instance.OpenUI<UIPopupMenu>();
        _uiButtonPopup.Init(data,btn1,btn2,btn3);
    }
    
    public void CloseButtonPopupUI()
    {
        Time.timeScale = 1;
        UIManager.Instance.CloseUI<UIPopupMenu>();
    }

    public void InputPopup(string title, Action<string> okeyCallback = null,Action noCallback = null)
    {
        if (_uiInputPopup == null) 
        {
            UIManager.Instance.OpenUI<UIInputPopup>();
            _uiInputPopup = UIManager.Instance.GetUI<UIInputPopup>().GetComponent<UIInputPopup>();
        }
        UIManager.Instance.OpenUI<UIInputPopup>();
        _uiInputPopup.Init(title,okeyCallback,noCallback);
    }

}

public class ButtonPopupData
{
    public string btnText1;
    public string btnText2;
    public string btnText3;
}

public class ConfirmData
{
    public string title;
    public string body;
}