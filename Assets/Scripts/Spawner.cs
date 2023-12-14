using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 

{
    public Wave[] waves;
    public Enemy enemy;

    Wave currentWave;
    int currentWaveNumber;

    int enemyRemainingToSpawn;
    int enemyRemainingAlive;
    float nextSpawnTime;

    Vector3 spawnPoisition = new Vector3 (2, 1,10);

void Start()
{
    NextWave();
}

void Update()
{
    if (enemyRemainingToSpawn > 0 && Time.time > nextSpawnTime)
    {
        enemyRemainingToSpawn--;
        nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

        Enemy spawnedEnemy = Instantiate (enemy, spawnPoisition, Quaternion.identity);
        spawnedEnemy.OnDeath += OnEnemyDeath;
    }
}

void OnEnemyDeath()
{
    enemyRemainingAlive--;

    if (enemyRemainingAlive == 0)
      {
        NextWave ();
    }
}

void NextWave()
{
    currentWaveNumber++;
   if (currentWaveNumber - 1 < waves.Length)
    {
        currentWave = waves [currentWaveNumber - 1];

        enemyRemainingToSpawn = currentWave.enemyCount;
        enemyRemainingAlive = enemyRemainingToSpawn;
    } 
}

[System.Serializable]
public class Wave
{
    public int enemyCount;
    public float timeBetweenSpawns;
}

}
