using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,IDamageable
{
    public static PlayerManager instance;
    public event System.Action<int> OnPlayerHit;
    public int health = 3;
    bool isPlayerAlive;

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
    public void TakeDamage(float Damage)
    {
        health -= (int)Damage;
        OnPlayerHit?.Invoke(health);
        if (health <= 0)
        {
            isPlayerAlive = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
