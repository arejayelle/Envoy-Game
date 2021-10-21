using UnityEngine;

    public class MaskProjectile : Projectile
    {
        protected override void onHit(EnemyLogic enemy)
        {
            enemy.maskUp();
        }
        
    }