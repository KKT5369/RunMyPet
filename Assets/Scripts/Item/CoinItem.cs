using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour,ItemBase
{
    [SerializeField] private ItemType itemType;
    readonly int _bronzeCoinScore = 10;
    readonly int _silverCoinScore = 20;
    readonly int _goldCoinScore = 30;
    
    
    public void Action()
    {
        switch (itemType)
        {
            case ItemType.BronzeCoin:
                AddCoin(_bronzeCoinScore);
                break;
            case ItemType.SilverCoin:
                AddCoin(_silverCoinScore);
                break;
            case ItemType.GoldCoin:
                AddCoin(_goldCoinScore);
                break;
        }
    }

    void AddCoin(int score)
    {
        GameManager.Instance.Score += score;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameManager.Instance.ItemAction(this);
        gameObject.SetActive(false);
    }
}
