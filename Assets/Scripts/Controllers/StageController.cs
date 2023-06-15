using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private MapBase _map;
    
    private void Awake()
    {
        GameManager.Instance.SettingMap(20);
        Init();
    }

    private void Init()
    {
        GameManager.Instance.GetMap();
    }
}
