using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [Tooltip("Prefab to spawn as an enemy.")]
    public GameObject enemyPrefab;

    [Tooltip("Transform that defines the spawn position. If empty, the spawner's own transform is used.")]
    public Transform spawnPoint;

    [Tooltip("Time in seconds between enemy spawns.")]
    public float spawnInterval = 1f;

    [Tooltip("Maximum number of enemies that can exist at once.")]
    public int maxEnemies = 1;

    [Tooltip("Spawn a first enemy immediately when the scene starts.")]
    public bool spawnOnStart = true;

    private float timer;
    private readonly List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Reset()
    {
        spawnPoint = transform;
    }

    private void Start()
    {
        if (spawnPoint == null)
        {
            spawnPoint = transform;
        }

        timer = spawnInterval;

        if (spawnOnStart)
        {
            SpawnEnemy();
        }
    }

    private void Update()
    {
        if (enemyPrefab == null || maxEnemies <= 0)
        {
            return;
        }

        CleanupDestroyedEnemies();

        if (spawnedEnemies.Count >= maxEnemies)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    public void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoint == null || spawnedEnemies.Count >= maxEnemies)
        {
            return;
        }

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation, transform);
        spawnedEnemies.Add(enemy);
    }

    private void CleanupDestroyedEnemies()
    {
        spawnedEnemies.RemoveAll(item => item == null);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(position, 0.25f);
        Gizmos.DrawLine(transform.position, position);
    }
}
