using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Infectable: MonoBehaviour
{
    [Header("Infectable")]
    // immunity
    [SerializeField]
    public bool isInfected;
    [Range(0, 100)] [SerializeField]
    protected int immunity = 20;
    
    // infection
    [Range(0, 2)] [SerializeField]
    protected float infectionRange = 0.7f;
    [Range(0, 5)] [SerializeField] 
    protected float infectionBonus = 2;
    [SerializeField]
    LayerMask whatToInfect;

    [SerializeField] protected float kInfectionCooldown= 5f;
    protected float infectionTimeLeft=0f;

    public void TickInfection(float risk)
    {
        if (isInfected || immunity >= 100) return;

        var randomPull = (Random.value * 100) + risk;
        if (randomPull > immunity)
        {
            isInfected = true;
            HandleInfection();
        }

    }

    protected abstract void HandleInfection();

    protected void SpreadInfection()
    {
        if (infectionTimeLeft <= 0)
        {
            InfectOthers();
            infectionTimeLeft = kInfectionCooldown;
        }
        else
        {
            infectionTimeLeft -= Time.deltaTime;
        }
    }
    
    protected void InfectOthers()
    {
        Collider2D[] thingsToInfect = Physics2D.OverlapCircleAll(transform.position, infectionRange, whatToInfect);
        for (int i = 0; i < thingsToInfect.Length; i++)
        {
            var infectable = thingsToInfect[i].GetComponent<Infectable>();
            infectable.TickInfection(infectionBonus);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, infectionRange);
    }
    
}