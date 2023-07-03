using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : SingleTon<GameManager>
{
    public Action gameInit;
    public SceneType purScene;
    public GameType gameType;
    
    private readonly float _defaultGameSpeed = 15f;
    private float _gameSpeed;
    private int _score;
    private PlayerController _playerController;
    public RankData rankData;
    
    [Header("맵 관련")]
    private List<GameObject> _mapPrefabs = new();
    private List<GameObject> _objMaps = new();
    private int stageIndex;
    private int mapQue;
    private GameObject playerGo;
    private float _distance;

    public PlayerController Player
    {
        get => _playerController;
    }
    
    public int Score
    {
        get => _score;
        set => _score = value;
    }
    
    public float Distance
    {
        get => _distance;
        set => _distance = value;
    }

    public float GameSpeed
    {
        get => _gameSpeed;
        set => _gameSpeed = value;
    }


    // 맵을 초기상태로 세팅 합니다.
    public void SettingMap(GameType gameType)
    {
        switch (gameType)
        {
            case GameType.Game2D:
                playerGo = Instantiate(ResourcesLoadManager.Instance.LoadCharacter("Player"));
                _playerController = playerGo.GetComponent<PlayerController>();
                _mapPrefabs = ResourcesLoadManager.Instance.LoadMap("");
                
                break;
            case GameType.Game3D:
                playerGo = Instantiate(ResourcesLoadManager.Instance.LoadCharacter("Player3D"));
                _playerController = playerGo.GetComponent<PlayerController>();
                _mapPrefabs = ResourcesLoadManager.Instance.LoadMap("");
                break;
        }
        
        mapQue = _mapPrefabs.Count;
        GameSpeedReset();
        _score = 0;
        stageIndex = 0;
        _distance = 0;
    }
    
    // 다음맵을 가져와서 생성하고 List로 저장 합니다.
    // 이전 맵은 비활성화 시킵니다.
    public void GetMap()
    {
        if (mapQue <= stageIndex)
        {
            EndGame();
            return;
        }
        else if (0 != stageIndex)
        {
            _objMaps[stageIndex-1].gameObject.SetActive(false);
        }
        _objMaps.Add(Instantiate(_mapPrefabs[stageIndex]));
        stageIndex++;
    }
    
    // 씬의 플레이어와 맵을 삭제하고 다시 세팅 합니다.
    public void Restart()
    {
        Destroy(playerGo);
        foreach (var go in _objMaps)
        {
            Destroy(go);
        }
        
        ItemManager.Instance.AllStopCoroutine();
        UIManager.Instance.CloseUI<UIGame>();
        _objMaps.Clear();
        gameInit.Invoke();
    }

    public void GameSpeedReset()
    {
        _gameSpeed = _defaultGameSpeed;
    }

    public void EndGame()
    {
        SoundManager.Instance.PlayUISound(SoundType.Clear);
        Time.timeScale = 0;
        ConfirmData data = new() { title = "뿌뿌뿌뿌이~!!!", body = $"{_score} 점 \n 랭킹에 이름을 남겨 볼까요?" };
        PopupManager.Instance.ConfirmPopup(data,
            (() =>
        {
            PopupManager.Instance.InputPopup("이름을 알려주세요",(value) =>
            {
                rankData = new()
                {
                    nicName = value,
                    score = _score,
                    disrance = (int)_distance,
                };
                DataManager.Instance.SetRankData(rankData);
                UIManager.Instance.CloseUI<UIInputPopup>();
                UIManager.Instance.OpenUI<UIRank>();
            },(() =>
            {
                UIManager.Instance.OpenUI<UIRank>();
            }));
        }),
            () =>
        {
            UIManager.Instance.OpenUI<UIRank>();
        });
        
        
        
    }
    
    
}