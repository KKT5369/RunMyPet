using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLoadManager : SingleTon<ResourcesLoadManager>
{
    private readonly string _mapPath = "Maps/";
    private readonly string _charPath = "Characters/";
    

    public List<GameObject> LoadMap()
    {
        //var go = Resources.Load<MapBase>(_path).gameObject;
        var go = Resources.LoadAll<MapBase>(_mapPath);
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
