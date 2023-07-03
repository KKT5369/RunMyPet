using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourcesLoadManager : SingleTon<ResourcesLoadManager>
{
    private readonly string _mapPath = "Maps/";
    private readonly string _charPath = "Characters/";
    

    public List<GameObject> LoadMap(string path)
    {
        string mapPath = _mapPath + path;
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
        string _path = _charPath + path;
        var go = Resources.Load<PlayerController>(_path).gameObject;
        return go;
    }
    
}
