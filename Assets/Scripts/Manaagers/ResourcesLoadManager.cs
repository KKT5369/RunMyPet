using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourcesLoadManager : SingleTon<ResourcesLoadManager>
{
    private readonly string _stage = "Stage";
    private readonly string _Char = "Char";

    private string _gameType;
    public List<GameObject> LoadMap()
    {
        _gameType = GameManager.Instance.gameType.ToString();
        string mapPath = $"{_gameType}/{_stage}";
        var go = Resources.LoadAll<MapBase>(mapPath);
        List<GameObject> gos = new List<GameObject>();

        foreach (var v in go)
        {
            gos.Add(v.gameObject);
        }

        return gos;
    }

    public GameObject LoadCharacter(string path)
    {
        _gameType = GameManager.Instance.gameType.ToString();
        string _path = $"{_gameType}/{_Char}/{path}";
        var go = Resources.Load(_path) as GameObject;
        return go;
    }

    public GameObject GetEndPoint()
    {
        _gameType = GameManager.Instance.gameType.ToString();
        var go = Resources.Load($"{_gameType}/EndPoint") as GameObject;
        
        return go;
    }
    
    
}
