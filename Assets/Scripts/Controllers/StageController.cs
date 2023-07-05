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
        if (GameManager.Instance.gameType == GameType.Game3D)
        {
            Camera.main.transform.position = new Vector3(-10, 8, -22);
            Camera.main.transform.rotation = Quaternion.Euler(20, 0, 0);
        }
    }

    private void Init()
    {
        Time.timeScale = 1;
        UIManager.Instance.OpenUI<UIGame>();
        GameManager.Instance.SettingMap();
        GameManager.Instance.GetMap();
        SoundManager.Instance.PlayBGM(SoundType.BGM);
    }
}

