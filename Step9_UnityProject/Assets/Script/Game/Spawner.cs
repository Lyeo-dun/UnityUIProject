using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Enemy enemy;


    Wave currentWave;
    int currentWaveNumber = 0;

    int enemiesRemainingToSpawn = 0;
    int enemiesRemaningAlive = 0;
    float nextSpawnTime;

    private void Start()
    {
        NextWave();
    }

    private void Update()
    {
        if(enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            Enemy spawnedEnemy = Instantiate<Enemy>(enemy, Vector3.zero, Quaternion.identity);
            spawnedEnemy.OnDeath += OnEnemyOnDeath;
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        if (currentWaveNumber - 1 < waves.Length) {
            currentWave = waves[currentWaveNumber - 1];

            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemaningAlive = enemiesRemainingToSpawn;
        }
    }

    void OnEnemyOnDeath()
    {
        enemiesRemaningAlive--;
        if(enemiesRemaningAlive == 0)
        {
            NextWave();
        }
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
    }

}
