using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    private int _score;
    public float _gameSpeed;
    private PlayerController _playerController;
    
    [Header("맵 관련")]
    private List<GameObject> mapGo = new();
    private GameObject purMapGo;
    private int stageIndex = 0;
    private int mapQue;
    
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

    private void Awake()
    {
        mapGo = ResourcesLoadManager.Instance.LoadMap();
        mapQue = mapGo.Count;
    }
    
    public void SettingMap(int gameSpeed)
    {
        GameSpeed = gameSpeed;
        var playerGo = Instantiate(ResourcesLoadManager.Instance.LoadCharacter("Player"));
        _playerController = playerGo.GetComponent<PlayerController>();
    }

    public void GetMap()
    {
        if (mapQue <= stageIndex)
        {
            Time.timeScale = 0;
            Debug.Log($"게임끝!! 스코어는~!? {_score}");
            return;
        }
        else if (stageIndex != 0)
        {
            purMapGo.gameObject.SetActive(false);
        }
        purMapGo = Instantiate(mapGo[stageIndex]);
        stageIndex++;
        _gameSpeed += 2f;
    }

    public void ItemAction(ItemBase itembase)
    {
        itembase.Action();
    }
}


