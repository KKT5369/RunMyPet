using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControoler : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float jumpPower = 200;

    private PlayerInput _playerInput;
    private PlayerAction _playerAction;



    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        //_playerInput.actions = _playerAction.Player.Move;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("점프");
        if (context.performed)
        {
            _rigidbody2D.AddForce(Vector2.up * (jumpPower * 200));
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        int layer = col.gameObject.layer;
        // if (LayerMask.NameToLayer("Floor") == layer)
        // {
        //     InputManager.Instance.isJump = false;
        //     InputManager.Instance.curJump = true;
        //     InputManager.Instance.doubleJimp = true;
        // }
    }
}
