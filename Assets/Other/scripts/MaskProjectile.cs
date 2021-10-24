using UnityEngine;

public class MaskProjectile : Projectile
    {
        protected override bool OnHit(EnemyLogic enemy)
        {
            return enemy.MaskUp();
        }
        
    }