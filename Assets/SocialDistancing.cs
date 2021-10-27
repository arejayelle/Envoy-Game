using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialDistancing : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] LineUpdater LineUpdater;
    [SerializeField] LineRenderer linerenderer;
    
    [SerializeField] float pullSpeed = 4f;
    [SerializeField] float yForce = 6f;
    private EnemyLogic pulledEnemy;

    public GameObject destroyEffect;
    [SerializeField] private LayerMask whatToPull;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        var firePointPosition = firePoint.position;

        var hitInfo = Physics2D.Raycast(firePointPosition, firePoint.right, whatToPull);
        if (hitInfo)
        {
            if (hitInfo.transform.CompareTag("Enemy"))
            {
                pulledEnemy = hitInfo.transform.GetComponent<EnemyLogic>();

                var pullRight = (pulledEnemy.transform.position.x - firePointPosition.x) > 0f;
                var xForce = (pullRight ? -1 : 1) * pullSpeed;
                
                hitInfo.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
                
                ScoreManager.instance.GainPoint(ScoreType.SocialDistancing);
                StartCoroutine(LineUpdater.PullTarget(pulledEnemy.transform));
            }
            else
            {
                StartCoroutine(LineUpdater.PullNothing(firePointPosition + firePoint.right * 50));
            }
        }
        else
        {
            StartCoroutine(LineUpdater.PullNothing(firePointPosition + firePoint.right * 50));
        }

    }
}