using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    private Animator mAnimator;

    private float cooldownTime;
    private float kWipeAttackCooldown;

    public Transform attackPos;
    public float attackRange;

    public LayerMask whatIsTables;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTime <= 0)
        {
            var isAttacking = Input.GetButton("Fire1");
            if (isAttacking)
            {
                Collider2D[] TablesToWipe = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsTables);
                for (int i = 0; i < TablesToWipe.Length; i++)
                {
                    TablesToWipe[i].GetComponent<Table>().cleanTable();
                }
                
                mAnimator.SetTrigger("WipeAttack");
            }
        }

    }
}
