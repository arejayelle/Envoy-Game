using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float speed;
    public float distance;
    public float lifeTime;
    public LayerMask whatIsSolid;

    public GameObject destroyEffect;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.forward, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                onHit(hitInfo.collider.GetComponent<EnemyLogic>());
            }
            DestroyProjectile();
        }
        
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    protected abstract void onHit(EnemyLogic enemy);
    void DestroyProjectile()
    {
        // Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
