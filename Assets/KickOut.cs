using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickOut : MonoBehaviour
{
    [SerializeField] Transform firePoint;

    [SerializeField] float yeetSpeed = 10f;
    [SerializeField] float yForce = 6f;

    [SerializeField] private LayerMask whatToPull;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shoo"))
        {
            Shoo();
        }
    }

    void Shoo()
    {
        var firePointPosition = firePoint.position;

        var hitInfo = Physics2D.Raycast(firePointPosition, firePoint.right,1f,whatToPull);
        if (hitInfo)
        {
            if (hitInfo.transform.CompareTag("Enemy"))
            {
                var enemy = hitInfo.transform.GetComponent<EnemyLogic>();

                var fromRight = (enemy.transform.position.x - firePointPosition.x) > 0f;
                var xForce = (fromRight ? 1 : -1) * yeetSpeed;
                
                hitInfo.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
                
            }
        }

    }
}