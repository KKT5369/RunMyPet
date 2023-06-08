using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private GameObject map;
    
    private void Awake()
    {
        GameManager.Instance.SettingMap("Map_01");
        map = GameManager.Instance.GetMap();
        Instantiate(map, transform);


    }
}
