using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    private Animator mAnimator;

    private float cooldownTime;
    private float kWipeAttackCooldown = 0.03f;

    public Transform attackPos;
    public float attackRange;

    public LayerMask whatIsTables;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTime <= 0)
        {
            var isAttacking = Input.GetButton("Fire1");
            if (isAttacking)
            {
                // Collider2D[] TablesToWipe = Physics2D.OverlapBox(attackPos.position, attackPos.localScale, 0f);
                Collider2D[] TablesToWipe = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsTables);
                for (int i = 0; i < TablesToWipe.Length; i++)
                {
                    var table = TablesToWipe[i].GetComponent<Table>();
                    table.CleanTable();
                }
                
                mAnimator.SetTrigger("WipeAttack");
            }

            cooldownTime = kWipeAttackCooldown;
        }
        else
        {
            cooldownTime -= Time.deltaTime;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        // Gizmos.DrawWireCube(attackPos.position, attackPos.localScale);
    }
}
