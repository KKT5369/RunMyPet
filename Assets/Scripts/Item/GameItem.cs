using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using Random = System.Random;

public class GameItem : MonoBehaviour,ItemBase
{ 
    private ItemType itemType;
    private Array _itemTypes;
    private float _itemTime = 5f;
    private float _saveGameSpeed;
    private Random _random = new();

    
    
    
    private void Awake()
    {
        _itemTypes = Enum.GetValues(typeof(ItemType));
        itemType = (ItemType)_itemTypes.GetValue(_random.Next(_itemTypes.Length));
    }

    public void Action()
    {
        Debug.Log($"{itemType.ToString()} 아이템");
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
        ItemManager.Instance.isOnMagnet = true;
        yield return new WaitForSeconds(_itemTime);
        ItemManager.Instance.isOnMagnet = false;
    }

    IEnumerator SpeedUp()
    {
        ItemManager.Instance.isSpeedup = true;
        _saveGameSpeed = GameManager.Instance.GameSpeed;
        GameManager.Instance.GameSpeed = _saveGameSpeed * 5;
        yield return new WaitForSeconds(_itemTime);
        GameManager.Instance.GameSpeed = _saveGameSpeed;
        ItemManager.Instance.isSpeedup = false;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        ItemManager.Instance.ItemAction(this);
        gameObject.SetActive(false);
    }
    
}
