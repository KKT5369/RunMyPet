using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupMenu : MonoBehaviour
{
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnRestart;
    [SerializeField] private Button btnLobby;
    [SerializeField] private Button btnClose;

    [SerializeField] private TMP_Text firstBtnTxt;
    [SerializeField] private TMP_Text twoBtnTxt;
    
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
        btnClose.onClick.AddListener((() => CloseUI()));
    }

    void LobyAddListener()
    {
        btnContinue.interactable = false;
        firstBtnTxt.text = "닉네임 변경";
        btnRestart.interactable = false;
        twoBtnTxt.text = "캐릭터 선택";
        btnLobby.onClick.AddListener((() =>
        {
            ConfirmData data = new ConfirmData() { title = "가려고?", body = "진짜 가려고?" };
            PopupManager.Instance.ConfirmPopup(data, () =>
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
            PopupManager.Instance.ConfirmPopup(data, () =>
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
