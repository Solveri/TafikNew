using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
   public static int waveNumber = 0;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float timeBetweenEnemies = 3;
    [SerializeField] int enemiesPerWave = 15;
    private EnemyMovement enemyMovement;
    bool isRoundGoingOn = false;
   public event System.Action<int> OnWaveEnded;
   
    List<EnemyMovement> enemies = new List<EnemyMovement>();
    private void OnEnable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        OnWaveEnded += CoinsManager.instance.CoinsOnEnd;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static int GetWaveCount()
    {
        return waveNumber;
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
            OnWaveEnded?.Invoke(waveNumber);
            waveNumber++;
            
            if (waveNumber == 4)
            {
                isRoundGoingOn = false;
                yield return null;
            }

        }

       
       

    }
}
