using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IAttackable
{
    public float Range { get; protected set; }
    public float Damage { get; protected set; }
    public float AttackCooldown { get; protected set; }

    public bool canAttack = true;
    public IDamageable Target { get; protected set; } // Target is now a property
    public event System.Action OnAttack;

    protected Projectile projectile;
    [SerializeField] protected float detectionRadius = 5f; // Radius for detecting enemies
    [SerializeField] private LayerMask enemyLayer; // Layer to detect enemies

    public float damage = 3f;
    protected float delayBetweenProjectiles = 0.3f;
    public Plot currentPlot;

    public Factions faction;
    protected Transform targetTransform; // Store the target's Transform separately
    public Ability ability;

    private void Awake()
    {
        ability = GetComponent<Ability>();
    }

    public virtual void Update()
    {
        DetectEnemies();
    }

    public virtual void Attack()
    {
        if (Target != null)
        {
            OnAttack?.Invoke();
        }
    }

    /// <summary>
    /// Detects the closest enemy within range and locks onto it.
    /// </summary>
    private void DetectEnemies()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        float closestDistance = float.MaxValue;
        Target = null; // Reset target before searching

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IDamageable>(out var damageable))
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    Target = damageable;
                    targetTransform = hit.transform;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the detection radius in the Scene View
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Draw a line to the target if available
        if (Target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetTransform.position);
        }
    }
}

public enum Factions
{
    Family,
    Enemy
}
