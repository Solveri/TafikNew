using System.Collections;
using UnityEngine;

public class BlackBabyTower : Tower
{
    [SerializeField] private Projectile projectilePrefab;
    private int numberOfProjectiles = 3;
   [SerializeField] AbilityBar abilityBar;


    private void Start()
    {
        projectile = projectilePrefab;
        AttackCooldown = 3f;
        canAttack = true;
        hasTarget = false;
        ability.requrieProjectileCount = 9;
        detectionRadius = 2f;
    }

    public override void Update()
    {
        base.Update();
        if (hasTarget)
        {
            if (ability.GetProjectileCount() >= 9)
            {
                ability.RestProjectile();
                abilityBar.SetBar(0,ability.requrieProjectileCount,ability.GetProjectileCount());
                ability.Activate();
            }

            Attack();
        }
    }

    public override void Attack()
    {
            if (canAttack)
            {
           
            StartCoroutine(AttackCooldownRoutine(targetTransform));
            }

            // Check if the target is still within range
            if (targetTransform != null && Vector2.Distance(transform.position, targetTransform.position) > detectionRadius || targetTransform == null)
            {
                Target = null;
                targetTransform = null;
                hasTarget = false;
            }
        
    }
    private IEnumerator AttackCooldownRoutine(Transform target)
    {
        canAttack = false;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            if (target != null)
            {
                Projectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                newProjectile.SetTarget(target,damage);
                ability.AddProjectile();
                abilityBar.SetBar(0, ability.requrieProjectileCount, ability.GetProjectileCount());
            }
            yield return new WaitForSeconds(delayBetweenProjectiles);
        }

        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }
    private void OnDrawGizmosSelected()
    {
        // Draw the detection radius in the editor for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
