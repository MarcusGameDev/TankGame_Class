using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int WaveNumber = 1;
    public int NumOfEnemies;
    public GameObject[] EnemiesTypes;
    public int EnemiesToSpawn;
    public int EnemiesRemaining;
    public Transform[] SpawnPoints;
    public float SpawnTimer;
    float SpawnTimerReset;
    bool WaveActive = false;

    public float waveTimer = 10;
    public float waveTimerReset = 10;

    private void Start()
    {
        SpawnTimerReset = SpawnTimer;

        WaveStart();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if there are enemies left to spawn
        if (EnemiesToSpawn > 0)
        {
            // Spawn timer counts down, if it hits 0, it then spawns an enemy.
            SpawnTimer -= Time.deltaTime;
            if (SpawnTimer < 0)
            {
                SpawnEnemy();
                SpawnTimer = SpawnTimerReset;
            }
        }
        
        // When the wave is inactive, it waits until the WaitTimer is finished then starts the next wave.
       if (WaveActive == false)
        {
            waveTimer -= Time.deltaTime;

            if(waveTimer < 0)
            {
                WaveStart();
            }
        }
    }

    // Starts the wave, and sets the new Waves values
     void WaveStart()
    {
        Debug.Log("Starting wave: " + WaveNumber);
        WaveNumber++;
        NumOfEnemies = WaveNumber;
        EnemiesToSpawn = NumOfEnemies;
        EnemiesRemaining = NumOfEnemies;
        WaveActive = true;
        waveTimer = waveTimerReset;
    }

     void SpawnEnemy()
    {
        // Pick An Enemy to Spawn
        int RandomRange = Random.Range(0, EnemiesTypes.Length);
        GameObject RandomEnemy = EnemiesTypes[RandomRange];

        // Pick a position to Spawn to
        int RandomRange2 = Random.Range(0, SpawnPoints.Length);
        Transform RandomSpawnPoint = SpawnPoints[RandomRange2];

        //  Instantiate()
        GameObject enemyTank = Instantiate(RandomEnemy, RandomSpawnPoint.position, RandomSpawnPoint.rotation);
        Debug.Log("Spawned " + RandomEnemy.name + " at " + RandomSpawnPoint.name);
        EnemiesToSpawn--;
    }

    // Enemy Tank will send message to this script, to count the enemies left for the wave. if 0 or less, it'll set a wave as inactive.
    public void EnemyDied()
    {
        EnemiesRemaining--;

        if(EnemiesRemaining >= 0)
        {
            WaveActive = false;
        }
    }
}
