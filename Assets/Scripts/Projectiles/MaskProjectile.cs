using System;
using UnityEngine;

public class MaskProjectile : Projectile
    {
        private void Awake()
        {
            mScoreType = ScoreType.EnemyMasked;
        }

        protected override bool OnHit(EnemyLogic enemy)
        {
            return enemy.MaskUp();
        }
        
    }