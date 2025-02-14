using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IAttackable
{
    public float BaseRange { get; private set; } = 5f;
    public float BaseDamage { get; private set; } = 3f;
    public float BaseAttackCooldown { get; protected set; } = 2f;
    public int BaseAbilityThreshold { get; protected set; } = 10;

    public float Range { get; protected set; }
    public float Damage { get; protected set; }
    public float AttackCooldown { get; private set; }
    public int AbilityThreshold { get; private set; }

    public bool canAttack = true;
    public IDamageable Target { get; protected set; }
    public event System.Action OnAttack;

    protected Projectile projectile;
    [SerializeField] protected float detectionRadius = 5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] SpriteRenderer spriteRenderer;

    protected float delayBetweenProjectiles = 0.3f;
    public Plot currentPlot;
    public Factions faction;
    protected Transform targetTransform;
    public Ability ability;
    public int StarLevel = 1; // Default to 1-star

    private void Awake()
    {
        ability = GetComponent<Ability>();
        // Apply star-level scaling
    }
    private void Start()
    {
       
    }
    public virtual void Update()
    {
        if (GameManager.Instance.isGamePasued)
        {
            return;
        }
        if (Target == null || !IsTargetInRange())
        {
            Target = null;
            DetectEnemies();
        }
    }

    public void ApplyStarStats()
    {
        float statMultiplier = GetStarMultiplier(StarLevel);

        Damage = BaseDamage * statMultiplier;
        AttackCooldown = BaseAttackCooldown / statMultiplier; // Lower cooldown for higher stars
        Range = BaseRange * statMultiplier;
        AbilityThreshold = Mathf.Max(1, Mathf.RoundToInt(BaseAbilityThreshold / statMultiplier)); // Less attacks needed for ability
        Debug.Log($"Tower stats updated: Damage={Damage}, Cooldown={AttackCooldown}, Range={Range}, AbilityThreshold={AbilityThreshold}");
    }

    private float GetStarMultiplier(int starLevel)
    {
        switch (starLevel)
        {
            case 1: return 1.0f;  // Normal Stats
            case 2: return 1.5f;  // +50% Boost
            case 3: return 2.0f;  // +100% Boost
            default: return 1.0f; // Safety fallback
        }
    }

    public virtual void Attack()
    {
        if (Target != null)
        {
            OnAttack?.Invoke();
        }
    }

    private void DetectEnemies()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        float closestDistance = float.MaxValue;
        Target = null;

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

    private bool IsTargetInRange()
    {
        if (targetTransform == null) return false;
        return Vector2.Distance(transform.position, targetTransform.position) <= detectionRadius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Range);

        if (Target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetTransform.position);
        }
    }
}

public enum Factions
{
    Player,
    Enemy
}