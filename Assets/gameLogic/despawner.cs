using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class despawner : MonoBehaviour
{
    private BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        bc = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            var enemy = other.transform.GetComponent<EnemyLogic>();
            if (!enemy.WasModified)
            {
                HandleUnmodified();
            }
            if(enemy!= null)
                enemy.Despawn();
        }
    }

    private void HandleUnmodified()
    {
        ScoreManager.instance.GainPoint(ScoreType.MissedOut);
    }
}
