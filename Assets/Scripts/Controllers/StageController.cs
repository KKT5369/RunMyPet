using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private MapBase _map;
    
    private void Awake()
    {
        GameManager.Instance.SettingMap(Stage.Map01,20);
        Init();
    }

    private void Init()
    {
        GameObject map = GameManager.Instance.GetMap();
        _map = Instantiate(map, transform).GetComponent<MapBase>();
        GameManager.Instance.AddCoin = _map.AddCoin;
        _map.speed = GameManager.Instance.GameSpeed;
    }
}
