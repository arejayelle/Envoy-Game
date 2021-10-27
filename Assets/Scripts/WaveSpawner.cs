using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] typeOfEnemies;

    [SerializeField] private float enemySpeed = 3f;
    [SerializeField] private float enemySpawnRate = 0.5f;

    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    }

    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private List<Transform> WaveEnemies;

    private int mWaveIndex = 0;

    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float initalWaitTime = 2f;
    float tillNextWave;

    private float searchCooldown = 1f;
    [SerializeField] public SpawnState state = SpawnState.COUNTING;

    // Start is called before the first frame update
    void Start()
    {
        tillNextWave = initalWaitTime;
        initializeWave();
    }

    void initializeWave()
    {
        var currentWave = waves[mWaveIndex];
        if (!currentWave.isActive)
        {
            WaveCompleted();
            return;
        }

        WaveEnemies = new List<Transform>();
        for (int i = 0; i < typeOfEnemies.Length; i++)
        {
            var numOfType = currentWave.numEnemies[i];
            for (int j = 0; j < numOfType; j++)
            {
                WaveEnemies.Add(typeOfEnemies[i]);
            }
        }
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

        while (WaveEnemies.Count > 0)
        {
            var spawnIndex = Random.Range(0, WaveEnemies.Count); // pick random index
            var toSpawn = WaveEnemies[spawnIndex]; // Get enemy
            SpawnEnemy(toSpawn); // Spawn
            WaveEnemies.RemoveAt(spawnIndex); // remove from list
            var spawnRate = wave.isMob ? enemySpawnRate + 1 : enemySpawnRate;

            yield return new WaitForSeconds(1f / spawnRate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var spawn = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation).GetComponent<EnemyLogic>();
        if (spawn != null)
        {
            spawn.speed = enemySpeed;
        }
    }

    bool EnemiesAreAlive()
    {
        searchCooldown -= Time.deltaTime;
        if (searchCooldown <= 0f)
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
            initNewRound();
        }

        initializeWave();
    }

    void initNewRound()
    {
        RoundManager.Instance.NewRound();
        var roundNumber = RoundManager.Instance.RoundNumber;

        RandomizeWaves();
        IncreaseSpeed(roundNumber);
        SetSpawnRate(roundNumber);
        IncreaseEnemies(roundNumber);

        mWaveIndex = 0;
        Debug.Log("Looping");
    }

    private void RandomizeWaves()
    {
        var morning = Random.Range(0, 1)>0;
        waves[0].isActive = morning;
        waves[1].isActive = !morning;
        var afternoon = Random.Range(0, 1)>0;
        waves[3].isActive = afternoon;
        waves[4].isActive = !afternoon;
        var evening = Random.Range(0, 1)>0;
        waves[6].isActive = evening;
        waves[7].isActive = !evening;
    }
    
    private void IncreaseSpeed(int roundNumber)
    {
        var zone = (roundNumber / 5f) + 1;

        if (zone <= 3)
        {
            enemySpeed = 3f * (zone);
        }
        else
        {
            enemySpeed = 10f;
        }
    }

    private void SetSpawnRate(int roundNumber)
    {
        var zone = (roundNumber / 5) + 1;

        // increase spawn rate
        if (zone < 2) enemySpawnRate = 0.5f;
        else if (zone < 5) enemySpawnRate = zone;
        else enemySpawnRate = 5;
    }

    private void IncreaseEnemies(int roundNumber)
    {
        for (var inW = 0; inW < waves.Length; inW++)
        {
            var wave = waves[inW];

            // increase spawns
            for (var inE = 0; inE < wave.numEnemies.Length; inE++)
            {
                var numEnemies = wave.numEnemies[inE];
                // be nice, only increase pre-existing enemies
                if (roundNumber < 5)
                {
                    if (numEnemies == 0) continue;

                    wave.numEnemies[inE] += 1;
                }
                // add all types of enemies
                else if (roundNumber < 10)
                {
                    wave.numEnemies[inE] += 1;
                }
                // everything is 10
                else
                {
                    wave.numEnemies[inE] = 10;
                }
            }
        }
    }
}