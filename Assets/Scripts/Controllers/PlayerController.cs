using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Rigidbody2D _rigidbody2D;
    public SpriteRenderer sprite;
    public float jumpPower = 8;
    private int _jumpIndex;
    private Vector2 _vector2;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
    #if UNITY_EDITOR
        transform.Translate(_vector2 * 10 * Time.fixedDeltaTime);
    #endif
    }

    public void OnJump(InputValue value)
    {
        if (_jumpIndex <= 2)
        {
            animator.SetBool("isRun",false);
            _rigidbody2D.velocity = Vector2.up * jumpPower;
            _jumpIndex++;
        }
    }

    public void OnMove(InputValue value)
    {
        _vector2 = value.Get<Vector2>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        int otherLayer = col.gameObject.layer;
        if (LayerMask.NameToLayer("Floor") == otherLayer)
        {
            animator.SetBool("isRun",true);
            _jumpIndex = 0;
        }

        if (LayerMask.NameToLayer("Enemy") == otherLayer)
        {
            _jumpIndex = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        int otherLayer = col.gameObject.layer;
        
        if (LayerMask.NameToLayer("EndPoint") == otherLayer)
        {
            GameManager.Instance.GetMap();
        }
        if (col.gameObject.name.Equals("FallCollider"))
        {
            GameManager.Instance.EndGame();
        }
    }
}


