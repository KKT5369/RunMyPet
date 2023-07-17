using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILoby : MonoBehaviour
{
    [SerializeField] private TMP_Text txtMaxScore;
    
    [SerializeField] private Button btnMenu;
    [SerializeField] private Button btnStart;
    [SerializeField] private Button btnRank;
    [SerializeField] private Button btnRankInit;

    
    public Action lobyMapSetting;
    public Action CharChange;
    private void Awake()
    {
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
            ButtonPopupData data = new() { btnText1 = "캐릭터 선택", btnText2 = "게임 모드",btnText3 = "게임 종료"};
            PopupManager.Instance.ButtonPopup(data,
            (() =>
            {
                UIManager.Instance.OpenUI<UICharacterSellect>();
            }), 
            (() =>
            {
                UIManager.Instance.OpenUI<UISelectMod>();
            }),
            (() => Application.Quit()));
        }));
        btnStart.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            ConfirmData confirmData = new ConfirmData(){ title = "게임시작", body = $" 오늘도 씐나게 달려 보십시다!! let's GO!!" };
            PopupManager.Instance.ConfirmPopup(confirmData,(() =>
            {
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
