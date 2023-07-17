using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : EnemyBase
{
    public float jumpForce = 20f;
    private float _jumpInterval = 2f;

    private void Start()
    {
        _jumpInterval = Random.Range(1f, 3f);
        InvokeRepeating("Jump", _jumpInterval, 2);
    }

    private void Jump()
    {
        if (!isDie)
        {
            switch (GameManager.Instance.gameType)
            {
                case GameType.Game2D:
                    _rig2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    break;
                case GameType.Game3D:
                    _rig.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
                    break;
            }
        }
    }
}
