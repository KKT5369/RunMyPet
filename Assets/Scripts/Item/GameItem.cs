using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class GameItem : MonoBehaviour,ItemBase
{ 
    private ItemType itemType;
    private Array _itemTypes;
    private float _itemTime = 3f;
    private float _saveGameSpeed;
    private Random _random = new();
    private UIGame _uiGame;
    
    private void Awake()
    {
          _itemTypes = Enum.GetValues(typeof(ItemType));
        itemType = (ItemType)_itemTypes.GetValue(_random.Next(_itemTypes.Length));
    }

    private void Start()
    {
        _uiGame = UIManager.Instance.GetUI<UIGame>().GetComponent<UIGame>();
    }

    public void Action()
    {
        switch (itemType)
        {
            case ItemType.SpeedItem:
                ItemManager.Instance.OnStartCoroutine(SpeedUp(),itemType);
                break;
            case ItemType.MagnetItem:
                ItemManager.Instance.OnStartCoroutine(OnMagnet(),itemType);
                break;
        }
    }

    IEnumerator OnMagnet()
    {
        _uiGame.ActiveBuff(itemType,true);
        ItemManager.Instance.isOnMagnet = true;
        yield return new WaitForSeconds(_itemTime);
        _uiGame.Fade(itemType);
        yield return new WaitForSeconds(2f);
        _uiGame.ActiveBuff(itemType,false);
        ItemManager.Instance.isOnMagnet = false;
        
    }

    IEnumerator SpeedUp()
    {
        _uiGame.ActiveBuff(itemType,true);
        ItemManager.Instance.isSpeedup = true;
        _saveGameSpeed = GameManager.Instance.GameSpeed;
        GameManager.Instance.GameSpeed = _saveGameSpeed * 2;
        yield return new WaitForSeconds(_itemTime);
        _uiGame.Fade(itemType);
        yield return new WaitForSeconds(3f);
        _uiGame.ActiveBuff(itemType,false);
        GameManager.Instance.GameSpeed = _saveGameSpeed;
        ItemManager.Instance.isSpeedup = false;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        ItemManager.Instance.ItemAction(this);
        gameObject.SetActive(false);
    }
    
}
