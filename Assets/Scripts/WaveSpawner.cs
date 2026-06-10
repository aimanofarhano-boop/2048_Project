using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;

    private Wave currentWave;

    [SerializeField] 
    private Transform[] spawnPoints;
    
    [SerializeField]
    private Transform canvasParent;

    [SerializeField]
    private float spawnInterval = 0.5f;

    private float timeBettweenSpawns;
    private int i = 0;

    private bool stopSpawning = false;
    private bool isSpawningWave = false;

    private void Awake()
    {
        currentWave = waves[i];
        timeBettweenSpawns = currentWave.TimeBeforeThisWave;
    }

    // Update is called once per frame
    void Update()
    {
        if(stopSpawning)
        {
            return;
        }

        if (Time.time >= timeBettweenSpawns && !isSpawningWave)
        {
            StartCoroutine(SpawnWaveRoutine());
        }
    }

    private IEnumerator SpawnWaveRoutine()
    {
        isSpawningWave = true;

        int spawnCount = Mathf.RoundToInt(currentWave.NumberToSpawn);
        for (int j = 0; j < spawnCount; j++)
        {
            int num = Random.Range(0, currentWave.EnemiesInWave.Length);
            int num2 = Random.Range(0, spawnPoints.Length);
            
            Instantiate(currentWave.EnemiesInWave[num], spawnPoints[num2].position, spawnPoints[num2].rotation, canvasParent);
            yield return new WaitForSeconds(spawnInterval);
        }

        IncWave();
        timeBettweenSpawns = Time.time + currentWave.TimeBeforeThisWave;
        isSpawningWave = false;
    }

    private void IncWave()
    {
        if (i + 1 < waves.Length)
        {
            i++;
            currentWave = waves[i];
        }
        else
        {
            stopSpawning = true;
        }
    }
}
