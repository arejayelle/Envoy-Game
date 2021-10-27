using System;
using UnityEngine;
using UnityEngine.Events;


public class BulletTimeSpawner : WaveSpawner
{

    protected override bool EnemiesAreAlive()
    {
        return base.EnemiesAreAlive();
    }

    private void Start()
    {
        initializeWave();
        DoBulletTime();
    }

    protected override void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        BulletTimeManager.instance.OnBulletTimeEnd();
    }

    protected override void DoBulletTime()
    {
        enemySpeed /= 2;
        foreach (var enTranform in DeployedEnemies)
        {
            var enemy = enTranform.GetComponent<EnemyLogic>();
            enemy.speed = enemySpeed;
        }
    }

    public void BulletTimeEnd()
    {
        enemySpeed *= 2;
        pruneDeployed();
        foreach (var enTranform in DeployedEnemies)
        {
            var enemy = enTranform.GetComponent<EnemyLogic>();
            enemy.speed = enemySpeed;
        }
        enabled = false;
    }
}