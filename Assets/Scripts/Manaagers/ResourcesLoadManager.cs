using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourcesLoadManager : SingleTon<ResourcesLoadManager>
{
    private readonly string _stage = "Stage";
    private readonly string _Char = "Char";

    public List<GameObject> LoadMap()
    {
        string gameType = GameManager.Instance.gameType.ToString();
        string mapPath = $"{gameType}/{_stage}";
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
        string gameType = GameManager.Instance.gameType.ToString();
        string _path = $"{gameType}/{_Char}/{path}";
        var go = Resources.Load(_path) as GameObject;
        return go;
    }
    
    
}
