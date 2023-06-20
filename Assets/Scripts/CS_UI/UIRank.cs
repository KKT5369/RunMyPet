using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRank : MonoBehaviour
{
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private GameObject rankItem;
    [SerializeField] private Button btnClose;
    
    [Header("MyRank")]
    [SerializeField] private TMP_Text txtNicName;
    [SerializeField] private TMP_Text txtDistance;
    [SerializeField] private TMP_Text txtScore;

    private List<GameObject> _rankDatas = new();
    
    private void Awake()
    {
        SetRanking();
        btnClose.onClick.AddListener((() => UIManager.Instance.CloseUI<UIRank>()));
    }

    void SetRanking()
    {
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
            txtNicName.text = GameManager.Instance.rankData.nicName;
            txtDistance.text = Convert.ToString(GameManager.Instance.rankData.disrance);
            txtScore.text = Convert.ToString(GameManager.Instance.rankData.score);    
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

    private void OnEnable()
    {
        SetRanking();
    }
}




