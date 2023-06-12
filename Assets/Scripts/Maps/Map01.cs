using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map01 : MapBase
{
    private void Awake()
    {
        base.SetMaps();
    }

    private void Update()
    {
        base.MoveMap(speed);
        base.MapSwitch();
    }

    public override void AddCoin()
    {
        GameManager.Instance.Score += 20;
    }
    
}
