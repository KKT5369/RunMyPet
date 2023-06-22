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
    
    private int _score;
    private float _gameSpeed;
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
    public void SettingMap()
    {
        playerGo = Instantiate(ResourcesLoadManager.Instance.LoadCharacter("Player"));
        _playerController = playerGo.GetComponent<PlayerController>();
        _mapPrefabs = ResourcesLoadManager.Instance.LoadMap();
        mapQue = _mapPrefabs.Count;
        _score = 0;
        _gameSpeed = 10;
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
        _gameSpeed += 2f;
    }
    
    // 씬의 플레이어와 맵을 삭제하고 다시 세팅 합니다.
    public void Restart()
    {
        Destroy(playerGo);
        foreach (var go in _objMaps)
        {
            Destroy(go);
        }
        
        _objMaps.Clear();
        gameInit.Invoke();
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        rankData = new()
        {
            nicName = PlayerPrefs.GetString("myNicName"),
            score = _score,
            disrance = (int)_distance,
        };
        DataManager.Instance.SetRankData(rankData);
        UIManager.Instance.OpenUI<UIRank>();
    }
    
    // 출동한 아이템의 이펙트를 실행 합니다.
    public void ItemAction(ItemBase itembase)
    {
        itembase.Action();
    }
    
    // 코루틴을 실행 해줍니다.
    public void OnStartCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}


