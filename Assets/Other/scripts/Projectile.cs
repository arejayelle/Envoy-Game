using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    public Rigidbody2D rb;
    public float doubleRange;
    public LayerMask whatToMask;


    public GameObject destroyEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Invoke("DestroyProjectile", lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            var beep = hitInfo.transform;
            Collider2D[] doubleUp = Physics2D.OverlapCircleAll(hitInfo.transform.position, doubleRange, whatToMask);

            
            for (int i = 0; i < doubleUp.Length ; i++)
            {
                var thing = doubleUp[i];
                if (thing.CompareTag("Enemy"))
                {
                    OnHit(thing.GetComponent<EnemyLogic>());
                }
            }

            OnHit(hitInfo.GetComponent<EnemyLogic>());
            DestroyProjectile();
        }
    }

    protected abstract void OnHit(EnemyLogic enemy);
    
    void DestroyProjectile()
    {
        // Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
