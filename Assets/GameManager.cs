using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGamePasued;
    public bool isGameOver;

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
}
