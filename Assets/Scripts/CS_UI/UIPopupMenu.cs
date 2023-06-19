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
    [SerializeField] private Button btnClose;
    
    void Start()
    {
        switch (GameManager.Instance.purScene)
        {
            case SceneType.LobyScene:
                LobyAddListener();
                break;
            case SceneType.GameScene:
                GameAddListener();
                break;
        }
        btnClose.onClick.AddListener((() => UIManager.Instance.CloseUI<UIPopupMenu>()));
    }

    void LobyAddListener()
    {
        btnContinue.interactable = false;
        btnContinue.GetComponent<Image>().color = Color.black;
        btnRestart.interactable = false;
        btnRestart.GetComponent<Image>().color = Color.black;
        btnLobby.onClick.AddListener((() =>
        {
            ConfirmData data = new ConfirmData() { title = "가려고?", body = "진짜 가려고?" };
            ConfirmManager.Instance.OpenPopup(data, () =>
            {
                Application.Quit();
            });
        }));
    }
    
    void GameAddListener()
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
            ConfirmManager.Instance.OpenPopup(data, () =>
            {
                
                SceneLoadManager.Instance.LoadScene(SceneType.LobyScene);
            });
        }));
    }

    void CloseUI()
    {
        Time.timeScale = 1;
        UIManager.Instance.CloseUI<UIPopupMenu>();
    }
}
