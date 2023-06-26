using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class CoinItem : MonoBehaviour,ItemBase
{
    [SerializeField] private ItemType itemType;
    readonly int _bronzeCoinScore = 10;
    readonly int _silverCoinScore = 20;
    readonly int _goldCoinScore = 30;
    private CircleCollider2D _collider2D;
    private void Awake()
    {
        _collider2D = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (GameManager.Instance.isFever)
        {
            _collider2D.radius = 3f;
        }
        else
        {
            _collider2D.radius = 0.2f;
        }
    }

    // ItemBase 인터페이스를 상속 받았으므로 해당 아이템의 이펙트를 작성 합니다.
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
    
    // 스코어를 게임매니져에 저장 합니다.
    void AddCoin(int score)
    {
        GameManager.Instance.Score += score;
    }

    // 트리거 발생시 Action 함수를 게임 매니저 에서 실해 해주고 게임오브젝트를 비활성화 합니다.
    private void OnTriggerEnter2D(Collider2D col)
    {
        GameManager.Instance.ItemAction(this);
        SoundManager.Instance.PlayEffect(SoundType.Sell,SoundVolume.coinEffect);
        transform.DOMove(col.transform.position, 0.1f).OnComplete((() => gameObject.SetActive(false)));
    }
}
