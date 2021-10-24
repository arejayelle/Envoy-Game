using UnityEngine;

    public class MaskProjectile : Projectile
    {
        protected override void OnHit(EnemyLogic enemy)
        {
            enemy.maskUp();
        }
        
    }