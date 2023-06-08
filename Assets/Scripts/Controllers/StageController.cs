using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private MapBase map;

    private void Awake()
    {
        GameManager.Instance.SettingMap("Map_01");
        GameObject go = GameManager.Instance.GetMap();
        Instantiate(go, transform);
    }
}
