using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int waveNumber = 0;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float timeBetweenEnemies = 3;
    [SerializeField] int enemiesPerWave = 1;
    private EnemyMovement enemyMovement;
    bool isRoundGoingOn = false;
    List<EnemyMovement> enemies = new List<EnemyMovement>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator SpawnRoutine()
    {
        isRoundGoingOn = true;
        while (isRoundGoingOn)
        {

            for (int i = 0; i < enemiesPerWave; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                enemyMovement = enemy.GetComponent<EnemyMovement>();
                enemies.Add(enemyMovement);
                isRoundGoingOn = true;
                yield return new WaitForSeconds(timeBetweenEnemies);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
            waveNumber++;
            enemiesPerWave += waveNumber;
            if (waveNumber == 4)
            {
                isRoundGoingOn = false;
                yield return null;
            }

        }

       
       

    }
}
