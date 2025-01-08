using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBabyTower : Tower
{
    [SerializeField] Projectile projectile;

    float delayBetweenProjectile = 0.3f;
    int numberOfProjectiles = 3;
    // Start is called before the first frame update
    void Start()
    {
        AttackCooldown = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
      

        if (collision.gameObject.CompareTag("Enemy") && !hasTarget)
        {

            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                hasTarget = true;
                Target = damageable;
                
            }
        }
        if (hasTarget)
        {
            if (canAttack)
            {
                
              StartCoroutine(AttackCooldownRoutine(collision.transform));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            if (damageable == Target)
            {
               
                Target = null;
                hasTarget = false;
            }

        }
    }
    private IEnumerator AttackCooldownRoutine(Transform tarns)
    {
        canAttack = false;
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Projectile newProjectile = Instantiate(projectile, this.transform.position, Quaternion.identity);
            newProjectile.SetTarget(tarns.transform);
            yield return new WaitForSeconds(delayBetweenProjectile);

        }
       

        
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }
}
