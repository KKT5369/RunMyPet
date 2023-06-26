using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void FixedUpdate()
    {
        transform.Translate(_vector2 * 10 * Time.fixedDeltaTime);

        
    }

    public void OnJump(InputValue value)
    {
        if (_jumpIndex <= 2)
        {
            animator.SetBool("isRun",false);
            _rigidbody2D.AddForce(Vector2.up * (jumpPower * 200));
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
        if (col.gameObject.name.Equals("EndPoint(Clone)"))
        {
            GameManager.Instance.GetMap();
        }
    }
}


