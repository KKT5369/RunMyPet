using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonMoveEnemy : MonoBehaviour
{
    private Rigidbody2D _rig;
    private BoxCollider2D _col;
    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer.Equals(8))
        {
            Debug.Log(col.contacts[0].normal.x);
            if (col.contacts[0].normal.x.Equals(0))
            {
                StartCoroutine(nameof(Die));
            }
            else
            {
                Debug.Log("플레이어 사망");
            }
        }
    }

    IEnumerator Die()
    {
        _rig.AddForce(Vector2.up * (2 * 500));
        yield return new WaitForSeconds(0.5f);
        _col.isTrigger = true;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
