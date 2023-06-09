using Common;
using DG.Tweening;
using UnityEngine;

public class CoinItem : MonoBehaviour,ItemBase
{
    [SerializeField] private CoinTyep coinTyep;
    private GameType _gameType;
    readonly int _bronzeCoinScore = 10;
    readonly int _silverCoinScore = 20;
    readonly int _goldCoinScore = 30;
    private CircleCollider2D _collider2D;
    private CapsuleCollider _collider;
    private void Awake()
    {
        _gameType = GameManager.Instance.gameType;
        switch (_gameType)
        {
            case GameType.Game2D:
                _collider2D = GetComponent<CircleCollider2D>();
                break;
            case GameType.Game3D:
                _collider = GetComponent<CapsuleCollider>();
                break;
        }
    }

    // ItemBase 인터페이스를 상속 받았으므로 해당 아이템의 이펙트를 작성 합니다.
    public void Action()
    {
        switch (coinTyep)
        {
            case CoinTyep.BronzeCoin:
                AddCoin(_bronzeCoinScore);
                break;
            case CoinTyep.SilverCoin:
                AddCoin(_silverCoinScore);
                break;
            case CoinTyep.GoldCoin:
                AddCoin(_goldCoinScore);
                break;
        }
    }

    private void FixedUpdate()
    {
        if (ItemManager.Instance.isOnMagnet)
        {
            switch (_gameType)
            {
                case GameType.Game2D:
                    _collider2D.radius = 4f;
                    break;
                case GameType.Game3D:
                    _collider.radius = 8f;
                    break;
            }
        }
        else
        {
            switch (_gameType)
            {
                case GameType.Game2D:
                    _collider2D.radius = 0.3f;
                    break;
                case GameType.Game3D:
                    _collider.radius = 0.3f;
                    break;
            }
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
        if (LayerMask.NameToLayer("Player") == col.gameObject.layer)
        {
            ItemManager.Instance.ItemAction(this);
            SoundManager.Instance.PlayEffect(SoundType.Sell,SoundVolume.coinEffect);
            transform.DOMove(col.transform.position, 0.1f).OnComplete((() => gameObject.SetActive(false)));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.NameToLayer("Player") == other.gameObject.layer)
        {
            ItemManager.Instance.ItemAction(this);
            SoundManager.Instance.PlayEffect(SoundType.Sell,SoundVolume.coinEffect);
            transform.DOMove(other.transform.position, 0.1f).OnComplete((() => gameObject.SetActive(false)));
        }
    }
}
