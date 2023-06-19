using System;

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

    public void ButtonPopup(ButtonPopupData data,Action btn1 ,Action btn2,Action btn3)
    {
        if (_uiButtonPopup == null)
        {
            UIManager.Instance.OpenUI<UIConfirm>();
            _uiButtonPopup = UIManager.Instance.GetUI<UIPopupMenu>().GetComponent<UIPopupMenu>();
        }
        UIManager.Instance.OpenUI<UIConfirm>();
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