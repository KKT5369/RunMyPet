using System;
using UnityEngine;

public class InputManager : SingleTon<InputManager>
{
    public Action jump;

    public bool isJump = false;
    
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!isJump)
            {
                Debug.Log("점프");
                jump.Invoke();
                isJump = true;
            }
        }
    }
}
