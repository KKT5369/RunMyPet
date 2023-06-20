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
            ButtonPopupData data = new() { btnText1 = "캐릭터 선택", btnText2 = "테스트",btnText3 = "게임 종료"};
            PopupManager.Instance.ButtonPopup(data,null,null,(() => Application.Quit()));
        }));
        btnTest.onClick.AddListener((() =>
        {
            ConfirmData confirmData = new() { title = "게임시작", body = "준비가 되었나요?" };
            PopupManager.Instance.ConfirmPopup(confirmData,(() => SceneLoadManager.Instance.LoadScene(SceneType.GameScene)));
        }));
                
    }
}
