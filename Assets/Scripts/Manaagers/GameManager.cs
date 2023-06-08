using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    private string _sellectMap;
    private int score;
    private Action addCoin;

    public int Score
    {
        get => score;
        set => score = value;
    }

    public Action AddCoin
    {
        get => addCoin;
        set => addCoin = value;
    }
    
    public void SettingMap(string sellectMap)
    {
        this._sellectMap = sellectMap;
    }

    public GameObject GetMap()
    {
        var go = ResourcesLoadManager.Instance.LoadMap(_sellectMap);
        return go;
    }
}
