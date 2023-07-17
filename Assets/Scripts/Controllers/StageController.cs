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
            Vector3 pos = new Vector3(5, 10, -30);
            Vector3 rot = new Vector3(5, 0.5f, 0);
            CameraManager.Instance.Setting(pos,rot);
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

