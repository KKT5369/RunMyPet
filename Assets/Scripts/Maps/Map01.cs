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
    }

    private void Update()
    {
        MoveMap(speed);
        MapSwitch();
    }
}
