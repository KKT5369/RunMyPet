using System;
using System.Collections.Generic;
using NetworkConstants;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameRankItem : MonoBehaviour
{
    [SerializeField] private Image imgProfile;
    [SerializeField] private Image imgRank;
    [SerializeField] private TMP_Text txtRankNum;
    [SerializeField] private TMP_Text txtNicName;
    [SerializeField] private TMP_Text txtScore;
    [SerializeField] private TMP_Text clearTime;
    [SerializeField] private RectTransform viewRect;
    
    public List<Sprite> rankImg = new();

    private void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(viewRect.rect.width - 50f, 100f);
        
        txtNicName.SetDefaultFont();
        txtScore.SetDefaultFont();
        clearTime.SetDefaultFont();
    }

    public void Setting(GameRankingD gameRanking)
    {
        if (gameRanking.gameRank < 4)
        {
            switch (gameRanking.gameRank)
            {
                case 1:
                    imgRank.sprite = rankImg[0];
                    break;
                case 2:
                    imgRank.sprite = rankImg[1];
                    break;
                case 3:
                    imgRank.sprite = rankImg[2];
                    break;
                
            }
            txtRankNum.gameObject.SetActive(false);
        }
        else
        {
            imgRank.gameObject.SetActive(false);
            txtRankNum.text = gameRanking.gameRank.ToString();
        }
        string url = $"{AwsUrl.pet}{gameRanking.petUserDId}/index.png";
        TextureManager.SetTexture(gameObject,imgProfile,url);
        
        txtNicName.text = gameRanking.nknmNm;
        txtScore.text = $"{gameRanking.gamePnt.ToString()} 점";
        clearTime.text = gameRanking.modDtm;
    }
}



[Serializable]
public class MyGameData
{
    public int gamePnt;
}



