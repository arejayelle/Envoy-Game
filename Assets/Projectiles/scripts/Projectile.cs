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
            // hit the target
            if (OnHit(hitInfo.GetComponent<EnemyLogic>()))
            {
                ScoreManager.instance.GainPoint(1);
                // splash damage
                Collider2D[] splashZone = Physics2D.OverlapCircleAll(hitInfo.transform.position, doubleRange, whatToMask);
                var numEnemies = 0;
                
                for (int i = 0; i < splashZone.Length ; i++)
                {
                    var thing = splashZone[i];
                    if (thing.CompareTag("Enemy"))
                    {
                        if(thing != hitInfo) // not initial target
                        {
                            if (OnHit(thing.GetComponent<EnemyLogic>()))
                                numEnemies++;
                        }
                    }
                } 
                if(numEnemies > 0) ScoreManager.instance.GainPoint(numEnemies +1);
            }
             
            DestroyProjectile();
        }
    }

    protected abstract bool OnHit(EnemyLogic enemy);
    
    void DestroyProjectile()
    {
        // Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
