using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Enemy prefabEnemy;

    public static int currentEnemyCount;

    private EnemySpawnPoint[] spawnPoints;

    void Start()
    {
        spawnPoints = FindObjectsOfType<EnemySpawnPoint>();
    }

    void Update()
    {
        if (currentEnemyCount < spawnPoints.Length)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(prefabEnemy.gameObject);
        newEnemy.transform.position = spawnPoints.RandomChoice().transform.position;
    }
}