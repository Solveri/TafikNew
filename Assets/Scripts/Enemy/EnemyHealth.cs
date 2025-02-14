using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour,IDamageable
{
    public float starting_health=5f;
    const float healthIncrease = 5f;
    public event System.Action<float> OnDamageTaken;
    public event System.Action OnDeath;
    private float health;
    // Formula for MaxHealth is health+= healthIncrease*currentWave

    public void TakeDamage(float Damage)
    {
        OnDamageTaken?.Invoke(Damage);
        health -= Damage;
        if (health <= 0)
        {
            health = 0;
            OnDeath?.Invoke();
            Destroy(this.gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        health = starting_health + (healthIncrease * EnemySpawner.waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnDestroy()
    {
      
    }
}
