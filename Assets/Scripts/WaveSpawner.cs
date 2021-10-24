using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    
    private enum SpawnState{
        SPAWNING,
        WAITING,
        COUNTING
    }
    
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;

    private int nextWaveIndex = 0;
    
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float countdown;
    
    private float searchCooldown = 1f;
    [SerializeField] SpawnState state = SpawnState.COUNTING;

    // Start is called before the first frame update
    void Start()
    {
        countdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemiesAreAlive())
            {
                WaveCompleted();
            }
            else
            {
                return; 
            }
        }
        if (countdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWaveIndex]));
            }
        }
        else
        {
            countdown -= Time.deltaTime;
        }
    }


    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
    
    bool EnemiesAreAlive()
    {
        searchCooldown -= Time.deltaTime;
        if(searchCooldown<=0f)
        {
            searchCooldown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        countdown = timeBetweenWaves;

        nextWaveIndex++;   
        
        if (nextWaveIndex == waves.Length)
        {
            nextWaveIndex = 0;
            Debug.Log("Looping");
        }
    }
}
