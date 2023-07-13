using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected Rigidbody2D _rig2D;
    protected BoxCollider2D _col2D;
    protected Rigidbody _rig;
    protected Collider _col;
    protected float force = 5;
    protected bool isDie;
    
    protected void Awake()
    {
        switch (GameManager.Instance.gameType)
        {
            case GameType.Game2D:
                _rig2D = GetComponent<Rigidbody2D>();
                _col2D = GetComponent<BoxCollider2D>();
                break;
            case GameType.Game3D:
                _rig = GetComponent<Rigidbody>();
                _col = GetComponent<Collider>();
                break;
        }
        
    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer.Equals(8))
        {
            Vector2 colPos = col.contacts[0].normal;
            if (ItemManager.Instance.isSpeedup || colPos.y <= -0.5f)
            {
                SoundManager.Instance.PlayEffect(SoundType.Grow);
                StartCoroutine(nameof(Die));
            }
            else
            {
                GameManager.Instance.EndGame();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer.Equals(8))
        {
            Vector2 colPos = collision.contacts[0].normal;
            if (ItemManager.Instance.isSpeedup || colPos.y <= -0.5f)
            {
                SoundManager.Instance.PlayEffect(SoundType.Grow);
                StartCoroutine(nameof(Die3D));
            }
            else
            {
                GameManager.Instance.EndGame();
            }
        }
    }

    IEnumerator Die()
    {
        if (isDie) yield break;
        isDie = true;
        _rig2D.AddForce(Vector2.up * (force * 500));
        yield return new WaitForSeconds(0.3f);
        _col2D.isTrigger = true;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    
    IEnumerator Die3D()
    {
        if (isDie) yield break;
        isDie = true;
        _rig.AddForce(Vector3.up * (force * 1000));
        yield return new WaitForSeconds(0.3f);
        _col.isTrigger = true;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
