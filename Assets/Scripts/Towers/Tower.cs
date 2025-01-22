using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public  class Tower : MonoBehaviour,IAttackable
{
    public  float Range { get; protected set; }
    public  float Damage { get; protected set; }
    public  float AttackCooldown { get; protected set; }
    public bool canAttack = true;
    public bool hasTarget = false;
    public IDamageable Target;
    public event System.Action OnAttack;
    protected Projectile projectile;
    protected float detectionRadius = 5f; // Radius for detecting enemies
    public LayerMask enemyLayer; // Layer to detect enemies
   public float damage = 3f;
    protected float delayBetweenProjectiles = 0.3f;
   
    
    protected Transform targetTransform; // Store the target's Transform separately
    public Ability ability;

    public virtual void Update()
    {
        EnemyDetetcion();
    }
    private void Awake()
    {
        ability = GetComponent<Ability>();  
    }
    public virtual void Attack()
    {
      
        OnAttack?.Invoke();
    }
    //Ability To Be Implemented

    void EnemyDetetcion()
    {

        if (!hasTarget)
        {
            // Detect enemies within the radius
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius,enemyLayer);

            foreach (var hit in hits)
            {
                Debug.Log("Hit: " + hit.name);
                if (hit.TryGetComponent<IDamageable>(out var damageable))
                {
                    hasTarget = true;
                    Target = damageable;
                    targetTransform = hit.transform; // Save the target's Transform
                    break; // Lock onto the first enemy
                }
            }
        }

        
    }
   
}
