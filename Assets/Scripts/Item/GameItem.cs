using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using Random = System.Random;

public class GameItem : MonoBehaviour,ItemBase
{
    [SerializeField] private ItemType itemType;
    private float _itemTime = 10f;
    
    private float _saveGameSpeed;

    public void Action()
    {
        switch (itemType)
        {
            case ItemType.SpeedItem:
                GameManager.Instance.OnStartCoroutine(SpeedUp());
                break;
            case ItemType.MagnetItem:
                GameManager.Instance.OnStartCoroutine(OnMagnet());
                break;
        }
    }

    IEnumerator OnMagnet()
    {
        GameManager.Instance.isOnMagnet = true;
        yield return new WaitForSeconds(_itemTime);
        GameManager.Instance.isOnMagnet = false;
    }

    IEnumerator SpeedUp()
    {
        _saveGameSpeed = GameManager.Instance.GameSpeed;
        GameManager.Instance.GameSpeed = _saveGameSpeed * 5;
        yield return new WaitForSeconds(_itemTime);
        GameManager.Instance.GameSpeed = _saveGameSpeed;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        GameManager.Instance.ItemAction(this);
        gameObject.SetActive(false);
    }
    
}
