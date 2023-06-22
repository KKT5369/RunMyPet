using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private void Awake()
    {
        Init();
        GameManager.Instance.gameInit = Init;
    }

    private void Init()
    {
        Time.timeScale = 1;
        GameManager.Instance.SettingMap();
        GameManager.Instance.GetMap();
        UIManager.Instance.OpenUI<UIGame>();
    }
}
