using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager instance;
    public int Coins { get; private set; }
    public event System.Action<int> OnCoinsChanged;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Coins = 75;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CoinsOnEnd(int wave)
    {
        Coins += wave * 15;
        Debug.Log(Coins); 
    }
    public void AddCoins(int amount)
    {
        Coins += amount;
        OnCoinsChanged?.Invoke(Coins);
    }
    public void RemoveCoins(int amount)
    {
        Coins -= amount;
        OnCoinsChanged?.Invoke(Coins);
    }
}
