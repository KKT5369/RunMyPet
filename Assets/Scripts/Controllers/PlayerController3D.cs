using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController3D : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Rigidbody _rigidbody;
    public float jumpPower = 20;
    private int _jumpIndex;
    private Vector2 _pos;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
#if UNITY_EDITOR
        transform.Translate(_pos * 10 * Time.fixedDeltaTime);
#endif
    }

    public void OnJump(InputValue value)
    {
        if (_jumpIndex <= 2)
        {
            animator.SetBool("isRun",false);
            animator.Play("Jump", -1, 0);
            _rigidbody.velocity = Vector2.up * jumpPower;
            _jumpIndex++;
        }
    }

    public void OnMove(InputValue value)
    {
        _pos = value.Get<Vector2>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        int otherLayer = collision.gameObject.layer;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("EndPoint(Clone)"))
        {
            GameManager.Instance.GetMap();
        }
        if (other.gameObject.name.Equals("FallCollider"))
        {
            GameManager.Instance.EndGame();
        }
    }
}
