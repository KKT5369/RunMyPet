using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownEnemy : EnemyBase
{
    
    public float moveSpeed = 3f;
    public float moveDistance = 2f;
    private float initialY;
    private bool movingUp = true;

    private void Start()
    {
        initialY = transform.position.y;
    }

    private void Update()
    {
        if(isDie) return;
        if (movingUp)
        {
            _rig.velocity = new Vector2(_rig.velocity.x, moveSpeed);
            if (transform.position.y >= initialY + moveDistance)
                movingUp = false;
        }
        else
        {
            _rig.velocity = new Vector2(_rig.velocity.x, -moveSpeed);
            if (transform.position.y <= initialY - moveDistance)
                movingUp = true;
        }
    }
    
}
