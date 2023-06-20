using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameRankItem : MonoBehaviour
{
    [SerializeField] private TMP_Text txtRankNum;
    [SerializeField] private TMP_Text txtNicName;
    [SerializeField] private TMP_Text txtScore;
    [SerializeField] private TMP_Text txtDistance;
    [SerializeField] private RectTransform viewRect;
    
    public List<Sprite> rankImg = new();

    private void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(viewRect.rect.width - 50f, 100f);
    }

    public void Setting(RankData data)
    {
        txtRankNum.text = Convert.ToString(data.rank);
        txtNicName.text = data.nicName;
        txtScore.text = Convert.ToString(data.score);
        txtDistance.text = Convert.ToString($"{data.disrance} M");
    }
}



