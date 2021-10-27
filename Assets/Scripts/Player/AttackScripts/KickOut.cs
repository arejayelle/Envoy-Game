using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Player;
using UnityEngine;

public class KickOut : MonoBehaviour
{
    [SerializeField] Transform firePoint;

    [SerializeField] float yeetSpeed = 10f;
    [SerializeField] float yForce = 3f;

    [SerializeField] private LayerMask whatToShoo;

    private PlayerController player;
    void Start()
    {
        player = transform.GetComponent<PlayerController>();
        mAnimator = GetComponent<Animator>();

    }

    public Animator mAnimator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shoo"))
        {
            if (player.state == PlayerState.Chilling)
            {
                player.state = PlayerState.Kicking;
                Kick();
            }
        }
    }

    void Kick()
    {
        var firePointPosition = firePoint.position;

        
        Collider2D[] thingsToKick = Physics2D.OverlapCircleAll(firePointPosition, 1f, whatToShoo);
        for (int i = 0; i < thingsToKick.Length; i++)
        {
            var kickable = thingsToKick[i];
            if (kickable.CompareTag("Enemy"))
            {
                var enemy = kickable.transform.GetComponent<EnemyLogic>();
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
                    enemy.transform.GetComponent<Rigidbody2D>()
                        .AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
                    ScoreManager.instance.GainPoint(ScoreType.EnemyKicked);

                }
            }

        }
        
        mAnimator.SetTrigger("kick");
        Invoke("RestoreState", .07f);
    }
    
    void RestoreState()
    {
        player.state = PlayerState.Chilling;

    }
}