using System;

public class ConfirmManager : SingleTon<ConfirmManager>
{
    private UIConfirm _UIconfirm;

    public void OpenPopup(ConfirmData confirmData, Action callback)
    {
        if (_UIconfirm == null)
        {
            UIManager.Instance.OpenUI<UIConfirm>();
            _UIconfirm = UIManager.Instance.GetUI<UIConfirm>().GetComponent<UIConfirm>();
        }
        UIManager.Instance.OpenUI<UIConfirm>();
        _UIconfirm.Init(confirmData,callback);
    }
}

public class ConfirmData
{
    public string title;
    public string body;
}