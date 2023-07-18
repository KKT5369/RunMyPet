using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectMod : MonoBehaviour
{
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnMod2D;
    [SerializeField] private Button btnMod3D;
    [SerializeField] private Button btnOkey;
    [SerializeField] private Button btnCancel;

    private GameType _selectGameType;
    
    
    private void Awake()
    {
        SetAddListener();
    }

    void SetAddListener()
    {
        btnClose.onClick.AddListener(Close);
        btnCancel.onClick.AddListener(Close);
        btnMod2D.onClick.AddListener((() => _selectGameType = GameType.Game2D));
        btnMod3D.onClick.AddListener((() => _selectGameType = GameType.Game3D));
        btnOkey.onClick.AddListener((() =>
        {
            ConfirmData data = new ConfirmData() 
                { title = $"경고!!", body = $"{_selectGameType.ToString()} 모드로 진행시켜!?" };
            PopupManager.Instance.ConfirmPopup(data,(() =>
            {
                GameManager.Instance.gameType = _selectGameType;
                UIManager.Instance.GetUI<UILoby>().GetComponent<UILoby>().lobyMapSetting.Invoke();
                Close();
            }));
        }));
    }

    void Close()
    {
        UIManager.Instance.CloseUI<UISelectMod>();
    }
}
