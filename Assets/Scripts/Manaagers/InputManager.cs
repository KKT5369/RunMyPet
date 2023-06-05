using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingleTon<InputManager>
{
    public Action jump;


    public bool curJump = true;

    private void Awake()
    {
    }
    
    void Started(InputAction.CallbackContext context)
    {
        Debug.Log("started!");
    }
    
    void FixedUpdate()
    {
        // if (curJump && Input.GetKey(KeyCode.Space))
        // {
        //     jump.Invoke();
        //     curJump = false;
        // }
    }
    
    
}
