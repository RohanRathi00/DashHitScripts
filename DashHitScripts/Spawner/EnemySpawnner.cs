using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public GameObject enemyGameObj; 
    int randomSpawnPoint;
    int randomEnemy;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, 1f);
    }

    void SpawnEnemy()
    {
        randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        randomEnemy = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity, enemyGameObj.transform);
    }
}
