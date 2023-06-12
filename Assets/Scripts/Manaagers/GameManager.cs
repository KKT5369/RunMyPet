using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    private Stage _sellectMap;
    private int _score;
    private Action _addCoin;
    private float _gameSpeed;

    public int Score
    {
        get => _score;
        set => _score = value;
    }

    public Action AddCoin
    {
        get => _addCoin;
        set => _addCoin = value;
    }
    
    public float GameSpeed
    {
        get => _gameSpeed;
        private set => _gameSpeed = value;
    }
    
    public void SettingMap(Stage Stage,int gameSpeed)
    {
        this._sellectMap = Stage;
        this.GameSpeed = gameSpeed;
    }

    public GameObject GetMap()
    {
        var go = ResourcesLoadManager.Instance.LoadMap(_sellectMap.ToString());
        return go;
    }
}

public enum Stage
{
    Map01,
    Map02,
    Map03,
}
