using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadTower : Tower
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] AbilityBar abilityBar;
    bool isAttacking;
    

    // Start is called before the first frame update
    void Start()
    {
        projectile = projectilePrefab;
        this.BaseAttackCooldown = 1f;
        canAttack = true;
       
        ability.requrieProjectileCount = 10;
        
    }

    // Update is called once per frame

    public override void Update()
    {
        base.Update();
        if (Target != null)
        {
            Attack();
        }

    }

    public override void Attack()
    {
        if (canAttack)
        {
            if (ability.GetProjectileCount() >= ability.requrieProjectileCount && !isAttacking)
            {
                ability.RestProjectile();
                abilityBar.SetBar(0, ability.requrieProjectileCount, ability.GetProjectileCount());
                ability.Activate(targetTransform.gameObject);
            }
            else
            {
            StartCoroutine(AttackCooldownRoutine(targetTransform));
            }
        }

        // Check if the target is still within range
        if (targetTransform != null && Vector2.Distance(transform.position, targetTransform.position) > detectionRadius || targetTransform == null)
        {
            Target = null;
            targetTransform = null;
           
        }
       

    }
    private IEnumerator AttackCooldownRoutine(Transform target)
    {
        canAttack = false;

        for (int i = 0; i < 1; i++)
        {
            if (target != null)
            {
                isAttacking = true;
                Projectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                newProjectile.SetTarget(target, this.Damage);
                //if the enemy is dead before all the projectiles are launched make the next projectiles next in target.
                ability.AddProjectile();
                abilityBar.SetBar(0, ability.requrieProjectileCount, ability.GetProjectileCount());

            }
            yield return new WaitForSeconds(delayBetweenProjectiles);
        }

        yield return new WaitForSeconds(AttackCooldown);
        isAttacking = false;
        canAttack = true;
    }
}
