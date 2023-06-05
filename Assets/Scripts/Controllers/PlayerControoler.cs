using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControoler : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float jumpPower = 200;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        InputManager.Instance.jump = Jump;
    }

    public void Jump()
    {
        Debug.Log("점프 실행.");
        _rigidbody2D.AddForce(Vector2.up * (jumpPower * 200));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        int layer = col.gameObject.layer;
        if (LayerMask.NameToLayer("Floor") == layer)
        {
            InputManager.Instance.isJump = false;
        }
    }
}
