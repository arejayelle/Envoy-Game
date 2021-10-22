using UnityEngine;

public abstract class Infectable: MonoBehaviour
{
    // immunity
    public bool isInfected;
    public int immunity = 20;
    
    // infection
    public float infectionRange = 0.7f;
    [Range(0, 15)]
    public int infectionStrength = 10;
    public LayerMask whatToInfect;
    
    public void tickInfection(int risk)
    {
        if (isInfected || immunity >= 100) return;

        var randomPull = Random.value * 100 + risk;
        if (randomPull > immunity)
        {
            isInfected = true;
            handleInfection();
        }

    }

    protected abstract void handleInfection();

    protected void infectOthers()
    {
        Collider2D[] thingsToInfect = Physics2D.OverlapCircleAll(transform.position, infectionRange, whatToInfect);
        for (int i = 0; i < thingsToInfect.Length; i++)
        {
            var infectable = thingsToInfect[i].GetComponent<Infectable>();
            infectable.tickInfection(infectionStrength);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, infectionRange);
    }
    
}