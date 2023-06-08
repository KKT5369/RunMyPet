using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map01 : MapBase
{
    public float speed;

    private void Awake()
    {
        speed = 10f;
        SetMaps();
        GameManager.Instance.AddCoin = AddCoin;
    }

    private void Update()
    {
        MoveMap(speed);
        MapSwitch();
    }

    void AddCoin()
    {
        GameManager.Instance.Score += 10;
    }
    
}
