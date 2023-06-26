using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : EnemyBase
{
    public float jumpForce = 20f;
    private float jumpInterval = 2f;

    private void Start()
    {
        jumpInterval = Random.Range(1f, 3f);
        InvokeRepeating("Jump", jumpInterval, 2);
    }

    private void Jump()
    {
        _rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
