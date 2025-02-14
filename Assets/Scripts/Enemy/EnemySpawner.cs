using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public int enemiesPerWave = 5;
    public float timeBetweenEnemies = 1f;
    public float timeBetweenWaves = 5f;
    public int MaxWaveNumber = 10;

    public Sprite[] enemySprites; // Assign in Inspector

    public static int waveNumber = 1;
    private bool isRoundGoingOn = false;
    private float spawnTimer = 0f;
    private float waveTimer = 0f;
    private int spawnedEnemies = 0;
    public static event System.Action<int> OnWaveEnded;
    private List<EnemyMovement> enemies = new List<EnemyMovement>();

    private void Start()
    {
        StartWave();
    }

    public int GetCurrentWave()
    {
        return waveNumber;
    }

    private void Update()
    {
        if (GameManager.Instance.isGamePasued || isRoundGoingOn == false)
            return;

        if (spawnedEnemies < enemiesPerWave)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= timeBetweenEnemies)
            {
                spawnTimer = 0f; // Reset spawn timer
                SpawnEnemy();
            }
        }
        else
        {
            waveTimer += Time.deltaTime;

            if (waveTimer >= timeBetweenWaves)
            {
                waveTimer = 0f; // Reset wave timer
                waveNumber++;

                OnWaveEnded?.Invoke(waveNumber);

                if (waveNumber >= MaxWaveNumber)
                {
                    isRoundGoingOn = false;
                }
                else
                {
                    spawnedEnemies = 0; // Reset for next wave
                }
            }
        }
    }

    public void StartWave()
    {
        if (!isRoundGoingOn)
        {
            isRoundGoingOn = true;
            waveNumber = 0;
            spawnedEnemies = 0;
            spawnTimer = 0f;
            waveTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        enemies.Add(enemyMovement);
        spawnedEnemies++;

        // Assign the correct sprite based on the wave number
        SpriteRenderer enemyRenderer = enemy.GetComponent<SpriteRenderer>();
        if (enemyRenderer != null)
        {
            enemyRenderer.sprite = GetEnemySpriteForWave(waveNumber);
            enemyRenderer.color = Color.red;
        }
    }

    private Sprite GetEnemySpriteForWave(int wave)
    {
        int spriteIndex = (wave) % 6; // Cycle through 6 sprites
        return enemySprites[spriteIndex];
    }
}
