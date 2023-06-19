using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupMenu : MonoBehaviour
{
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnRestart;
    [SerializeField] private Button btnLobby;
    
    void Start()
    {
        SetAddListener();
    }

    void SetAddListener()
    {
        btnContinue.onClick.AddListener((() =>
        {
            CloseUI();
        }));
        btnRestart.onClick.AddListener((() =>
        {
            GameManager.Instance.Restart();
            CloseUI();
        }));
        btnLobby.onClick.AddListener((() =>
        {
            ConfirmData data = new ConfirmData() { title = "로비로?", body = "진짜 로비로 가려고?" };
            ConfirmManager.Instance.OpenPopup(data,(() => CloseUI()));
        }));
    }

    void CloseUI()
    {
        Time.timeScale = 1;
        UIManager.Instance.CloseUI<UIPopupMenu>();
    }
}
