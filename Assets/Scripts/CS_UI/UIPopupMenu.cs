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
            Time.timeScale = 1;
            UIManager.Instance.CloseUI<UIPopupMenu>();
        }));
        
    }
}
