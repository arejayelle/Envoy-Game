using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickOut : MonoBehaviour
{
    [SerializeField] Transform firePoint;

    [SerializeField] float yeetSpeed = 10f;
    [SerializeField] float yForce = 3f;

    [SerializeField] private LayerMask whatToShoo;


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

        var hitInfo = Physics2D.Raycast(firePointPosition, firePoint.right, 1f, whatToShoo);
        if (hitInfo)
        {
            if (hitInfo.transform.CompareTag("Enemy"))
            {
                var enemy = hitInfo.transform.GetComponent<EnemyLogic>();
                if (enemy.isInfected)
                {
                    enemy.WasModified = true;
                    GameManager.instance.kickCounter();
                    var fromRight = (enemy.transform.position.x - firePointPosition.x) > 0f;

                    if ((fromRight && !enemy.isMovingRight) || // they are on your right and moving left
                        (!fromRight && enemy.isMovingRight)) // they are on your left and moving right
                    {
                        enemy.Flip();
                    }

                    var xForce = (fromRight ? 1 : -1) * yeetSpeed;
                    hitInfo.transform.GetComponent<Rigidbody2D>()
                        .AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
                }
            }
        }
    }
}