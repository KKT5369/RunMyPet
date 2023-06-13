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
        base.MoveMap(GameManager.Instance._gameSpeed);
        base.MapSwitch();
        if (Input.GetKey(KeyCode.W))
        {
            GameManager.Instance._gameSpeed += 2;
        }
    }
}
