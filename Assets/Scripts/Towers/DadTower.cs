using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadTower : Tower
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] AbilityBar abilityBar;


    // Start is called before the first frame update
    void Start()
    {
        projectile = projectilePrefab;
        AttackCooldown = 1f;
        canAttack = true;
        hasTarget = false;
        ability.requrieProjectileCount = 10;
    }

    // Update is called once per frame

    public override void Update()
    {
        base.Update();
        if (hasTarget)
        {
            Attack();
        }

    }

    public override void Attack()
    {
        if (canAttack)
        {
            if (ability.GetProjectileCount() >= ability.requrieProjectileCount)
            {
                ability.RestProjectile();
                abilityBar.SetBar(0, ability.requrieProjectileCount, ability.GetProjectileCount());
                ability.Activate();
            }
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

        for (int i = 0; i < 1; i++)
        {
            if (target != null)
            {
                Projectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                newProjectile.SetTarget(target, damage);
                //if the enemy is dead before all the projectiles are launched make the next projectiles next in target.
                ability.AddProjectile();
                abilityBar.SetBar(0, ability.requrieProjectileCount, ability.GetProjectileCount());

            }
            yield return new WaitForSeconds(delayBetweenProjectiles);
        }

        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }
}
