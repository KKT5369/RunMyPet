using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    private Stage _sellectMap;
    private int _score;
    public float _gameSpeed;
    private PlayerController _playerController;
    
    public PlayerController Player
    {
        get => _playerController;
    }
    
    public int Score
    {
        get => _score;
        set => _score = value;
    }

    public float GameSpeed
    {
        get => _gameSpeed;
        private set => _gameSpeed = value;
    }
    
    public void SettingMap(Stage Stage,int gameSpeed)
    {
        _sellectMap = Stage;
        GameSpeed = gameSpeed;
        var playerGo = Instantiate(ResourcesLoadManager.Instance.LoadCharacter("Player"));
        _playerController = playerGo.GetComponent<PlayerController>();
    }

    public GameObject GetMap()
    {
        var mapGo = ResourcesLoadManager.Instance.LoadMap(_sellectMap.ToString());
        return mapGo;
    }

    public void ItemAction(ItemBase itembase)
    {
        itembase.Action();
    }
}

public enum Stage
{
    Map01,
    Map02,
    Map03,
}
