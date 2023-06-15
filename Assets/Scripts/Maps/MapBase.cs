using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapBase : MonoBehaviour
{
    public float speed;
    protected GameObject[] floors;
    private float floorPosx = 0;
    private float _pos;
    private int index = 0;
    protected float pivotPos = -50;

    [SerializeField] private Transform pieceTransform;

    private void Awake()
    {
        SetMaps();
    }

    private void Update()
    {
        MoveMap(GameManager.Instance._gameSpeed);
        MapSwitch();
        if (Input.GetKey(KeyCode.W))
        {
            GameManager.Instance._gameSpeed += 2;
        }
    }

    protected void SetMaps()
    {
        int childCount = pieceTransform.childCount;
        floors = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            GameObject floor = pieceTransform.GetChild(i).gameObject;
            floor.transform.position = new Vector3(floorPosx - 3,-6);
            floorPosx += 50;
            floors[i] = floor;
        }

        var go = Resources.Load("EndPoint");
        Instantiate(go, floors[childCount - 1].transform);
    }

    protected void MapSwitch()
    {
        if (index == floors.Length - 2)
        {
            return;
        }
        _pos = pieceTransform.position.x;
        if (pivotPos - 5f > _pos)
        {
            pivotPos -= 50;
            floors[index].gameObject.SetActive(false);
            floors[index + 2].gameObject.SetActive(true);
            index++;
        }
    }

    public void MoveMap(float speed)
    {
        pieceTransform.position += Vector3.left * speed * Time.deltaTime;
    }
}
