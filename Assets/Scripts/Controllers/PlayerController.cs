using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float jumpPower = 2;
    private int _jumpIndex;
    private Vector2 _vector2;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void OnJump(InputValue value)
    {
        if (_jumpIndex == 0)
        {
            _jumpIndex++;
            _rigidbody2D.AddForce(Vector2.up * (jumpPower * 200));
        }
        else if (_jumpIndex == 1)
        {
            _jumpIndex++;
            _rigidbody2D.AddForce(Vector2.up * (jumpPower * 100));
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        int layer = col.gameObject.layer;
        if (LayerMask.NameToLayer("Floor") == layer)
        {
            _jumpIndex = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        int layer = col.gameObject.layer;

        if (LayerMask.NameToLayer("Coin") == layer)
        {
            col.gameObject.SetActive(false);
            GameManager.Instance.AddCoin.Invoke();
            Debug.Log("코인");
        }
        else if (col.gameObject.name.Equals("EndPoint(Clone)"))
        {
            Debug.Log($"게임종료!! 스코어는 ==> {GameManager.Instance.Score}");
            Time.timeScale = 0;
        }
    }
}

enum Layer
{
    Coin = 7,
}
