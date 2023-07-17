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
        switch (GameManager.Instance.gameType)
        {
            case GameType.Game2D:
                Move2D();
                break;
            case GameType.Game3D:
                Move3D();
                break;
        }
    }

    void Move2D()
    {
        if (movingUp)
        {
            _rig2D.velocity = new Vector2(_rig2D.velocity.x, moveSpeed);
            if (transform.position.y >= initialY + moveDistance)
                movingUp = false;
        }
        else
        {
            _rig2D.velocity = new Vector2(_rig2D.velocity.x, -moveSpeed);
            if (transform.position.y <= initialY - moveDistance)
                movingUp = true;
        }
    }
    
    void Move3D()
    {
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
