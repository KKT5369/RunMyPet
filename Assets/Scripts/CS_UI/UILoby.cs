using UnityEngine;
using UnityEngine.UI;

public class UILoby : MonoBehaviour
{
    [SerializeField] private Button btnMenu;
    [SerializeField] private Button btnTest;

    private void Awake()
    {
        btnMenu.onClick.AddListener((() =>
        {
            UIManager.Instance.OpenUI<UIPopupMenu>();
        }));
        btnTest.onClick.AddListener((() =>
        {
            ConfirmData confirmData = new() { title = "게임시작", body = "준비가 되었나요?" };
            PopupManager.Instance.ConfirmPopup(confirmData,(() => SceneLoadManager.Instance.LoadScene(SceneType.GameScene)));
        }));
                
    }
}
