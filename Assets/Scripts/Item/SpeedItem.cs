using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SpeedItem : MonoBehaviour,ItemBase
{
    [SerializeField] private ItemType itemType;

    private float _saveGameSpeed;
    
    public void Action()
    {
        switch (itemType)
        {
            case ItemType.SpeedItem:
                GameManager.Instance.OnStartCoroutine(SpeedUp());
                break;
        }
    }

    IEnumerator SpeedUp()
    {
        _saveGameSpeed = GameManager.Instance._gameSpeed;
        GameManager.Instance._gameSpeed = _saveGameSpeed * 5;
        yield return new WaitForSeconds(2f);
        GameManager.Instance._gameSpeed = _saveGameSpeed;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        GameManager.Instance.ItemAction(this);
        gameObject.SetActive(false);
    }
    
}
