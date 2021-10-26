using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] typeOfEnemies;

    private enum SpawnState{
        SPAWNING,
        WAITING,
        COUNTING
    }
    
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private List<Transform> WaveEnemies;

    [SerializeField] private PauseMenu pauseMenu;
    private int round = 0;

    private int mWaveIndex = 0;
    
    [SerializeField] float timeBetweenWaves = 5f;
    float tillNextWave;
    
    private float searchCooldown = 1f;
    [SerializeField] SpawnState state = SpawnState.COUNTING;

    // Start is called before the first frame update
    void Start()
    {
        tillNextWave = 2f;
        initializeWave();
    }

    void initializeWave()
    {
        var currentWave = waves[mWaveIndex];

        WaveEnemies = new List<Transform>();
        for (int i = 0; i < typeOfEnemies.Length; i++)
        {
            var numOfType = currentWave.numEnemies[i];
            for (int j = 0; j < numOfType; j++)
            {
                WaveEnemies.Add(typeOfEnemies[i]);
            }
        }

        currentWave.count = WaveEnemies.Count;
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
        if (tillNextWave <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[mWaveIndex]));
            }
        }
        else
        {
            tillNextWave -= Time.deltaTime;
        }
    }


    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;
        for (int i = 0; i < wave.count; i++)
        {
            var spawnIndex = Random.Range(0, WaveEnemies.Count); // pick random index
            var toSpawn = WaveEnemies[spawnIndex]; // Get enemy
            SpawnEnemy(toSpawn); // Spawn
            WaveEnemies.RemoveAt(spawnIndex); // remove from list
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
        tillNextWave = timeBetweenWaves;

        mWaveIndex++;   
        
        if (mWaveIndex == waves.Length)
        {
            mWaveIndex = 0;

            round++;
            RoundManager.instance.NewRound();
            Debug.Log("Looping");
        }
        initializeWave();
    }
}
