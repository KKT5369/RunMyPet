using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerController : MonoBehaviour
{
    private float _jumpHeight = 15f;
    private float _gravity = -20f;

    private float _speed;
    private int _jumpIndex;
    private CharacterController _controller;

    private Vector3 _moveDir;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (_controller.isGrounded)
        {
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
            _moveDir.y = _jumpHeight;
            _jumpIndex++;
        }
    }
}
