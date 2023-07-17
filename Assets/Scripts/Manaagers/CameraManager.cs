using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SingleTon<CameraManager>
{
    private bool _mod;
    public void Setting(Vector3 pos ,Vector3 rot)
    {
        _mod = GameManager.Instance.gameType == GameType.Game2D;
        Camera.main.orthographic = _mod;
        Camera.main.transform.position = pos;
        Camera.main.transform.rotation = Quaternion.Euler(rot);
    }
    
}
