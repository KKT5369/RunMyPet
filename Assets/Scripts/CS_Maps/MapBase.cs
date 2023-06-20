using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class MapBase : MonoBehaviour
{
    private float speed;
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
        speed = GameManager.Instance._gameSpeed;
        MoveMap(speed);
        MapSwitch();
        GameManager.Instance.Distance += 0.02f;
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

        var finshObj = Resources.Load("EndPoint");
        var go = Instantiate(finshObj, floors[childCount - 1].transform) as GameObject;
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
