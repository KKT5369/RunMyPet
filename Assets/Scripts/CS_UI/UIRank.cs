using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRank : MonoBehaviour
{
    [SerializeField] private RectTransform bodyRect;
    [SerializeField] private GameObject bottomItem;

    [Header("랭크 아이템")]
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private GameObject rankItem;

    [Header("버튼")]
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnReStart;
    [SerializeField] private Button btnGoLobby;

    [Header("MyRank")] 
    [SerializeField] private TMP_Text txtRank; 
    [SerializeField] private TMP_Text txtNicName;
    [SerializeField] private TMP_Text txtDistance;
    [SerializeField] private TMP_Text txtScore;

    private List<GameObject> _rankDatas = new();
    
    private void Awake()
    {
        SetAddListener();
        SetRanking();
    }

    void SetAddListener()
    {
        btnClose.onClick.AddListener((() => UIManager.Instance.CloseUI<UIRank>()));
        btnReStart.onClick.AddListener((() =>
        {
            GameManager.Instance.Restart();
            UIManager.Instance.CloseUI<UIRank>();
        }));
        btnGoLobby.onClick.AddListener((() =>
        {
            ConfirmData data = new ConfirmData() { title = "로비로?", body = "진짜 로비로 가려고?" };
            PopupManager.Instance.ConfirmPopup(data, () =>
            {
                SceneLoadManager.Instance.LoadScene(SceneType.LobyScene);
            });
        }));
    }
    
    void SetRanking()
    {
        ButtonActive();
        KillRankData();
        RankSaveData rankSaveData = DataManager.Instance.GetRankData();
        List<RankData> rankDatas = rankSaveData.datas;

        foreach (var data in rankDatas)
        {
            var go = Instantiate(rankItem, contentRect);
            GameRankItem script = go.GetComponent<GameRankItem>();
            script.Setting(data);
            go.SetActive(true);
            _rankDatas.Add(go);
        }

        if (GameManager.Instance.rankData != null)
        {
            txtRank.text = Convert.ToString(GameManager.Instance.rankData.rank);
            txtNicName.text = GameManager.Instance.rankData.nicName;
            txtDistance.text = Convert.ToString($"{GameManager.Instance.rankData.disrance} M");
            txtScore.text = Convert.ToString($"{GameManager.Instance.rankData.score} 점");
        }
    }

    void KillRankData()
    {
        if (_rankDatas == null) return;

        foreach (var data in _rankDatas)
        {
            Destroy(data);
        }

        _rankDatas.Clear();
    }

    void ButtonActive()
    {
        switch (GameManager.Instance.purScene)
        {
            case SceneType.LobyScene:
                bodyRect.offsetMin = new Vector2(bodyRect.offsetMin.x, 10);
                bottomItem.SetActive(false);
                break;
            case SceneType.GameScene:
                bodyRect.offsetMin = new Vector2(bodyRect.offsetMin.x, 100);
                bottomItem.SetActive(true);
                break;
        }

        
    }

    private void OnEnable()
    {
        SetRanking();
    }
}




