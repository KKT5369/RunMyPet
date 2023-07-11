using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController3D : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private CharacterController _controller;
    
    private float _jumpHeight = 15f;
    private float _gravity = -20f;

    private float _speed;
    private int _jumpIndex;

    private Vector3 _moveDir;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (_controller.isGrounded)
        {
            animator.SetBool("isRun",true);
            _speed = 0f;
            _jumpIndex = 0;
        }

        _moveDir.y += _gravity * Time.deltaTime;
 
        // 캐릭터 움직임.
        _controller.Move(_moveDir * Time.deltaTime);
    }

    public void OnJump(InputValue value)
    {
        if (_jumpIndex < 2)
        {
            animator.SetBool("isRun",false);
            _moveDir.y = _jumpHeight;
            _jumpIndex++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        int otherLayer = collision.gameObject.layer;

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
