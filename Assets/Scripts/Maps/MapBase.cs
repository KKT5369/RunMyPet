using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBase : MonoBehaviour
{
    protected GameObject[] floors;
    private float _pos;
    private int index = 0;
    private float pivotPos = -30;

    protected void SetMaps()
    {
        int childCount = transform.childCount;
        floors = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            floors[i] = transform.GetChild(i).gameObject;
        }
    }

    protected void MapSwitch()
    {
        if (index == floors.Length - 2)
        {
            return;
        }
        _pos = transform.position.x;
        if (pivotPos > _pos)
        {
            pivotPos -= 30;
            floors[index].gameObject.SetActive(false);
            floors[index + 2].gameObject.SetActive(true);
            index++;
        }
    }

    protected void MoveMap(float speed)
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
    
    
    
}
