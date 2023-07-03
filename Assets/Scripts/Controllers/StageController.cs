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
        UIManager.Instance.OpenUI<UIGame>();
        GameManager.Instance.SettingMap(GameManager.Instance.gameType);
        GameManager.Instance.GetMap();
        SoundManager.Instance.PlayBGM(SoundType.BGM);
    }
}

