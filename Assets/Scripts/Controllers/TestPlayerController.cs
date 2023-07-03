using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerController : MonoBehaviour
{
    public float jumpHeight = 3f;
    public float jumpDuration = 0.5f;
    public float gravity = -5.8f;

    private float verticalSpeed = 0f;
    private bool isJumping = false;
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
            
            
            
        }

        if (isJumping)
        {
            verticalSpeed += gravity * Time.deltaTime;

            if (verticalSpeed < 0f)
                isJumping = false;

            Vector3 motion = new Vector3(0f, verticalSpeed, 0f);
            controller.Move(motion * Time.deltaTime);
        }
        
        _moveDir.y += gravity * Time.deltaTime;
 
        // 캐릭터 움직임.
        controller.Move(_moveDir * Time.deltaTime);
    }

    public void OnJump(InputValue value)
    {
        _moveDir.y = 10f;
    }
}
