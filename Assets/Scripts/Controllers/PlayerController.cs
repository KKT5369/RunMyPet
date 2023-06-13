using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator animator;
    public float jumpPower = 2;
    private int _jumpIndex;
    private Vector2 _vector2;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void OnJump(InputValue value)
    {
        if (_jumpIndex == 0)
        {
            animator.SetBool("isRun",false);
            _jumpIndex++;
            _rigidbody2D.AddForce(Vector2.up * (jumpPower * 200));
            
        }
        else if (_jumpIndex == 1)
        {
            animator.SetBool("isRun",false);
            _jumpIndex++;
            _rigidbody2D.AddForce(Vector2.up * (jumpPower * 100));
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        int otherLayer = col.gameObject.layer;
        if (LayerMask.NameToLayer("Floor") == otherLayer)
        {
            animator.SetBool("isRun",true);
            _jumpIndex = 0;
        }
        else if (otherLayer == (int)LayerNum.Wall)
        {
            Debug.Log("꽈광");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("EndPoint(Clone)"))
        {
            Debug.Log($"게임종료!! 스코어는 ==> {GameManager.Instance.Score}");
            Time.timeScale = 0;
        }
    }
}

enum LayerNum
{
    Coin = 7,
    Wall = 8,
}
