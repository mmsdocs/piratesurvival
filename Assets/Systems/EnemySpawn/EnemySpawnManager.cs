using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private float spawnTime = 0.0f;
    private float startSpawnTime = 0.0f;
    private List<SpawnPoint> spawnPoints;

    private void Start()
    {
        spawnTime = PlayerPrefs.GetFloat("enemySpawnTime", 1.0f);
        startSpawnTime = Time.timeSinceLevelLoad;
        spawnPoints = new List<SpawnPoint>();

        foreach (Transform point in transform) 
        {
            SpawnPoint spawnPoint = point.GetComponent<SpawnPoint>();
            spawnPoints.Add(spawnPoint);
        }
    }

    private void Update() { if (Time.timeSinceLevelLoad - startSpawnTime >= spawnTime) Spawn(); }

    private void Spawn()
    {
        int index = Random.Range(0, spawnPoints.Count);
        spawnPoints[index].Spawn();
        startSpawnTime = Time.timeSinceLevelLoad;
    }
}
