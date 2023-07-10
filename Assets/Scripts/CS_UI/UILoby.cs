using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILoby : MonoBehaviour
{
    [SerializeField] private TMP_Text txtMaxScore;
    
    [SerializeField] private Button btnMenu;
    [SerializeField] private Button btnStart;
    [SerializeField] private Button btnStart3dGame;
    [SerializeField] private Button btnRank;
    [SerializeField] private Button btnRankInit;

    private void Awake()
    {
        SoundManager.Instance.PlayBGM(SoundType.BGM);
        SetAddListener();
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
            ButtonPopupData data = new() { btnText1 = "캐릭터 선택(미구현)", btnText2 = "사운드 설정",btnText3 = "게임 종료"};
            PopupManager.Instance.ButtonPopup(data,
            (() =>
            {
                UIManager.Instance.OpenUI<UICharacterSellect>();
            }), 
            (() =>
            {
                ConfirmData data = new() { title = "그딴거", body = "없다" };
                PopupManager.Instance.ConfirmPopup(data);
            }),
            (() => Application.Quit()));
        }));
        btnStart.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            ConfirmData confirmData = new ConfirmData(){ title = "게임시작", body = $" 오늘도 씐나게 달려 보십시다!! let's GO!!" };
            PopupManager.Instance.ConfirmPopup(confirmData,(() =>
            {
                GameManager.Instance.gameType = GameType.Game2D;
                SceneLoadManager.Instance.LoadScene(SceneType.GameScene);
            }));
        }));
        btnStart3dGame.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            ConfirmData confirmData = new ConfirmData(){ title = "게임시작", body = $" 오늘도 씐나게 달려 보십시다!! let's GO!!" };
            PopupManager.Instance.ConfirmPopup(confirmData,(() =>
            {
                GameManager.Instance.gameType = GameType.Game3D;
                SceneLoadManager.Instance.LoadScene(SceneType.GameScene);
            }));
        }));
        btnRank.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            UIManager.Instance.OpenUI<UIRank>();
        }));
        btnRankInit.onClick.AddListener((() => DataManager.Instance.DeleteRank()));
    }
}
