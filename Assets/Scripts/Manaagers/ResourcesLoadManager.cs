using System;
using UnityEngine;

public class ResourcesLoadManager : SingleTon<ResourcesLoadManager>
{
    private readonly string _mapPath = "Maps/";
    

    public GameObject LoadMap(string path)
    {
        string _path = _mapPath + path;
        var go = Resources.Load<MapBase>(_path).gameObject;
        return go;
    }
    
}
