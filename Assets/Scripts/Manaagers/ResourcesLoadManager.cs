using System;
using UnityEngine;

public class ResourcesLoadManager : SingleTon<ResourcesLoadManager>
{
    private readonly string _mapPath = "Maps/";
    private readonly string _charPath = "Characters/";
    

    public GameObject LoadMap(string path)
    {
        string _path = _mapPath + path;
        var go = Resources.Load<MapBase>(_path).gameObject;
        return go;
    }

    public GameObject LoadCharacter(string path)
    {
        string _path = _charPath + path;
        var go = Resources.Load<PlayerController>(_path).gameObject;
        return null;
    }
    
}
