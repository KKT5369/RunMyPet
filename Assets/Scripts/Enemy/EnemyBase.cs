using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected Rigidbody2D _rig;
    protected BoxCollider2D _col;
    protected float force = 5;
    protected bool isDie;
    
    protected void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();
    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer.Equals(8))
        {
            Vector2 colPos = col.contacts[0].normal;
            if (colPos.y <= -0.5f)
            {
                SoundManager.Instance.PlayEffect(SoundType.Grow);
                StartCoroutine(nameof(Die));
            }
            else
            {
                //GameManager.Instance.EndGame();
            }
        }
    }

    IEnumerator Die()
    {
        if (isDie) yield break;
        isDie = true;
        _rig.AddForce(Vector2.up * (force * 500));
        yield return new WaitForSeconds(0.3f);
        _col.isTrigger = true;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
