using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField]List<Image> Hearts = new List<Image>();
    [SerializeField] EnemySpawner enemySpawner;
    float time;
   
    // Start is called before the first frame update
    void Start()
    {
        CoinsManager.instance.OnCoinsChanged += ChangeCoinText;
        PlayerManager.instance.OnPlayerHit += DisableHeartOnHit;
        EnemySpawner.OnWaveEnded += ChangeWave;
        ChangeWave(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGamePasued)
        {
            return;
        }
        time += Time.deltaTime;
        timerText.text = time.ToString("F2");
        
    }
    void ChangeCoinText(int X)
    {
        coinsText.text = "Coins: " + CoinsManager.instance.Coins;
    }
    void ChangeWave(int x)
    {
        waveText.text = "Wave" + enemySpawner.GetCurrentWave() + "/" + enemySpawner.MaxWaveNumber;
    }
    void DisableHeartOnHit(int index)
    {
        if (index <0)
        {
            return;
        }
        
        Image heart = Hearts[index];
        Debug.Log(index);

        if (heart != null)
        {
            heart.enabled = false;
        }
        
       

    }
}
