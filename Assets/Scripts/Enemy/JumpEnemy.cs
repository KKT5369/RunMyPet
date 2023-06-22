using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : EnemyBase
{
    public float jumpForce = 20f;
    public float jumpInterval = 2f;

    private void Start()
    {
        InvokeRepeating("Jump", jumpInterval, jumpInterval);
    }

    private void Jump()
    {
        _rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
