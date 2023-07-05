using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerController : MonoBehaviour
{
    private float jumpHeight = 10f;
    private float jumpDuration = 0.5f;
    private float gravity = -20f;

    private float verticalSpeed = 0f;
    private bool isJumping = false;
    private int _jumpIndex;
    private CharacterController controller;

    private Vector3 _moveDir;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            verticalSpeed = 0f;
            _jumpIndex = 0;
        }

        _moveDir.y += gravity * Time.deltaTime;
 
        // 캐릭터 움직임.
        controller.Move(_moveDir * Time.deltaTime);
    }

    public void OnJump(InputValue value)
    {
        if (_jumpIndex < 3)
        {
            _moveDir.y = jumpHeight;
            _jumpIndex++;
        }
    }
}
