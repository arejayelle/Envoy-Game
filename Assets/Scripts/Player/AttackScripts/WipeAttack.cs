using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Player;
using UnityEngine;
using UnityEngine.Serialization;

public class WipeAttack : MonoBehaviour
{
    
    private Animator mAnimator;

    private float cooldownTime;
    private float kWipeAttackCooldown = 0.03f;

    public Transform attackPos;
    public float attackRange;

    public LayerMask whatToWipe;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        player = transform.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTime <= 0)
        {
            var isAttacking = Input.GetButton("Fire3");
            if (isAttacking)
            {
                if (player.state == PlayerState.Chilling)
                {
                    player.state = PlayerState.Wiping;
                    Wipe();
                    
                }
                
            }

            cooldownTime = kWipeAttackCooldown;
        }
        else
        {
            cooldownTime -= Time.deltaTime;
        }

    }

    void Wipe()
    {
        Collider2D[] thingsToWipe = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatToWipe);
        for (int i = 0; i < thingsToWipe.Length; i++)
        {
            var thing = thingsToWipe[i];
            if (thing.CompareTag("wipeable"))
            {
                var wipeable = thing.GetComponent<IWipeable>();
                wipeable.Wipe();
            }

        }
        mAnimator.SetTrigger("wipe");
        Invoke("RestoreState", .1f);
    }
    
    void RestoreState()
    {
        player.state = PlayerState.Chilling;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        // Gizmos.DrawWireCube(attackPos.position, attackPos.localScale);
    }
}
