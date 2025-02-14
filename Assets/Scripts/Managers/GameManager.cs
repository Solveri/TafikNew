using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGamePasued;
    public bool isGameOver;
    [SerializeField] public GameObject prefabPop; // Ensure this is assigned in the Inspector
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isGamePasued = false;
    }
    public void PauseGame()
    {
        isGamePasued = !isGamePasued;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Restart()
    {
       //enemySpawer.start wavw
       //need to rest the timer and wave counter
       // need to rest the coins
       //need to clear the bench and plots
    }
}
