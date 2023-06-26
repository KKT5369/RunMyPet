using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILoby : MonoBehaviour
{
    [SerializeField] private TMP_Text txtNicName;
    [SerializeField] private TMP_Text txtMaxScore;
    
    [SerializeField] private Button btnMenu;
    [SerializeField] private Button btnStart;
    [SerializeField] private Button btnRank;
    [SerializeField] private Button btnRankInit;

    private readonly string _nicNameKey = "myNicName";
    
    private void Awake()
    {
        SoundManager.Instance.PlayBGM(SoundType.BGM);
        SetAddListener();
        if (!PlayerPrefs.HasKey(_nicNameKey))
        {
            ConfirmData data = new() { title = "아이디없음", body = "이런... 닉네임이 없어요 지금 만들러 가볼까요?" };
            PopupManager.Instance.ConfirmPopup(data,(() =>
            {
                NicCheck("닉네임 생성");
            }));
        }
        else
        {
            txtNicName.text = PlayerPrefs.GetString(_nicNameKey);
        }
    }

    void NicCheck(string title)
    {
        PopupManager.Instance.InputPopup(title,(value) =>
        {
            PlayerPrefs.SetString(_nicNameKey, value);
            txtNicName.text = value;
            UIManager.Instance.CloseUI<UIInputPopup>();
        });
    }

    public void SetScore()
    {
        txtMaxScore.text = GameManager.Instance.Score == 0 ?  "-" : Convert.ToString(GameManager.Instance.Score);
    }

    private void OnEnable()
    {
        SetScore();
    }

    void SetAddListener()
    {
        btnMenu.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            ButtonPopupData data = new() { btnText1 = "캐릭터 선택(미구현)", btnText2 = "닉네임 변경",btnText3 = "게임 종료"};
            PopupManager.Instance.ButtonPopup(data,
            (() =>
            {
                
            }), 
            (() =>
            {
                NicCheck("닉네임 변경");
            }),
            (() => Application.Quit()));
        }));
        btnStart.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            ConfirmData confirmData = new() { title = "게임시작", body = $"{txtNicName.text} 님 달릴 준비가 됐나요?" };
            PopupManager.Instance.ConfirmPopup(confirmData,(() => SceneLoadManager.Instance.LoadScene(SceneType.GameScene)));
        }));
        btnRank.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            UIManager.Instance.OpenUI<UIRank>();
        }));
        btnRankInit.onClick.AddListener((() => DataManager.Instance.DeleteRank()));
    }
}
