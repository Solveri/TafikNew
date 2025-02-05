using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherTower : Tower
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] AbilityBar abilityBar;
    bool isAttacking;
    float timePassed = 0f;
    float timeCoolDown = 0f;
    float attackInterval = 1f; // Attack every 1 second
    float lastAttackTime = 0f;
    float attackDuration = 5f; // Example attack duration

    void Start()
    {
        projectile = projectilePrefab;
        AttackCooldown = 3f;
        canAttack = true;
        
        ability.requrieProjectileCount = 10;
    }

    public override void Update()
    {
        base.Update();
        if (!canAttack)
        {
            timeCoolDown+= Time.deltaTime;
            if (timeCoolDown >= AttackCooldown)
            {
                Debug.Log("Attack enabled");
                canAttack = true;
                timeCoolDown = 0f;
                timePassed = 0;
                lastAttackTime = 0;
            }
        }
        if (timePassed >= attackDuration)
        {
            canAttack = false;
            
            return; // Stop attacking after duration ends
        }

        timePassed += Time.deltaTime;

        if (timePassed - lastAttackTime >= attackInterval)
        {
            lastAttackTime += attackInterval; // Move forward by 1 second
            Debug.Log("About tot attack");
            Attack(); // Call your attack function
        }
    }

    public override void Attack()
    {
        if (Target != null)
        {
            Target.TakeDamage(5f);
        }
    }
    public void OnDrawGizmos()
    {
        if (targetTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetTransform.position);
        }
    }
}
