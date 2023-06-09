using UnityEngine;

public class MapBase : MonoBehaviour
{
    private float speed;
    protected GameObject[] floors;
    private float floorPosx = 0;
    private float _pos;
    private int index = 0;
    protected float pivotPos = -50;
    
    private int _floorActiveNum = 4;  // 켜져있는 floor 개수

    [SerializeField] private Transform pieceTransform;

    private void Awake()
    {
        SetMaps();
    }

    private void Update()
    {
        speed = GameManager.Instance.GameSpeed;
        MoveMap(speed);
        MapSwitch();
        GameManager.Instance.Distance += 0.02f;
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.W))
        {
            GameManager.Instance.GameSpeed += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            GameManager.Instance.GameSpeed -= 1;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            GameManager.Instance.GameSpeed = 0f;
        }
        if (Input.GetKey(KeyCode.V))
        {
            ItemManager.Instance.isSpeedup = true;
        }
        
#endif
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

        for (int i = 0; i < _floorActiveNum; i++)
        {
            floors[i].gameObject.SetActive(true);
        }

        var finshObj = ResourcesLoadManager.Instance.GetEndPoint();
        Instantiate(finshObj, floors[childCount - 2].transform);
    }

    protected void MapSwitch()
    {
        if (index == floors.Length - _floorActiveNum)
        {
            return;
        }
        _pos = pieceTransform.position.x;
        if (pivotPos - 5f > _pos)
        {
            pivotPos -= 50;
            floors[index].gameObject.SetActive(false);
            floors[index + _floorActiveNum].gameObject.SetActive(true);
            index++;
        }
    }

    public void MoveMap(float speed)
    {
        pieceTransform.position += Vector3.left * speed * Time.deltaTime;
    }
}
