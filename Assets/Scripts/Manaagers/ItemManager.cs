using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class ItemManager : SingleTon<ItemManager>
{
    public bool isSpeedup;
    public bool isOnMagnet;
    
    private Coroutine isCorSpeedUp;
    private Coroutine isCorMagnet;
    
    
    // 충돌한 아이템의 이펙트를 실행 합니다.
    public void ItemAction(ItemBase itembase)
    {
        itembase.Action();
    }

    private Coroutine _isCoroutine;
    
    // 코루틴을 실행 해줍니다.
    public void OnStartCoroutine(IEnumerator coroutine,ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.SpeedItem:
                if (isCorSpeedUp != null)
                {
                    GameManager.Instance.GameSpeedReset();
                    StopCoroutine(isCorSpeedUp);
                }
                isCorSpeedUp = StartCoroutine(coroutine);
                break;
            case ItemType.MagnetItem:
                if (isCorMagnet != null)
                {
                    StopCoroutine(isCorMagnet);
                }
                isCorMagnet = StartCoroutine(coroutine);
                break;
        }
    }

    public void AllStopCoroutine()
    {
        if (isCorSpeedUp != null)
        {
            StopCoroutine(isCorSpeedUp);
            GameManager.Instance.GameSpeedReset();
        }

        if (isCorMagnet != null)
        {
            StopCoroutine(isCorMagnet);
            isOnMagnet = false;
        }
    }
    
}
