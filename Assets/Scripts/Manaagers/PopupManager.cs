using System;
using JetBrains.Annotations;
using UnityEngine;

public class PopupManager : SingleTon<PopupManager>
{
    private UIConfirm _UIconfirm;
    private UIPopupMenu _uiButtonPopup;

    public void ConfirmPopup(ConfirmData data, Action callback)
    {
        if (_UIconfirm == null)
        {
            UIManager.Instance.OpenUI<UIConfirm>();
            _UIconfirm = UIManager.Instance.GetUI<UIConfirm>().GetComponent<UIConfirm>();
        }
        UIManager.Instance.OpenUI<UIConfirm>();
        _UIconfirm.Init(data,callback);
    }

    public void ButtonPopup(ButtonPopupData data,[CanBeNull] Action btn1,[CanBeNull] Action btn2,[CanBeNull] Action btn3)
    {
        if (_uiButtonPopup == null) 
        {
            UIManager.Instance.OpenUI<UIPopupMenu>();
            _uiButtonPopup = UIManager.Instance.GetUI<UIPopupMenu>().GetComponent<UIPopupMenu>();
        }
        UIManager.Instance.OpenUI<UIPopupMenu>();
        _uiButtonPopup.AddListener(data,btn1,btn2,btn3);
    }
    
    public void CloseButtonPopupUI()
    {
        Time.timeScale = 1;
        UIManager.Instance.CloseUI<UIPopupMenu>();
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