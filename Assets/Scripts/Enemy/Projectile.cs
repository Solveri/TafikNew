using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public class Projectile : MonoBehaviour
{
    [SerializeField] public float speed = 10f; // Speed of the projectile
    [SerializeField] public float lifetime = 5f; // Time before the projectile is destroyed
    protected Transform target; // The target the projectile will move toward
   protected float damage;

    public void SetTarget(Transform newTarget,float damage)
    {
        this.damage = damage;
        target = newTarget;
    }

    private void Start()
    {
        // Destroy the projectile after a set time if it doesn’t hit anything
        //Destroy(gameObject, lifetime);
    }

    public virtual void Update()
    {
        if (GameManager.Instance.isGamePasued)
        {
            return;
        }
        if (target == null)
        {
            // Destroy the projectile if the target is missing
            Destroy(gameObject);
            return;
        }

        // Move the projectile toward the target
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Optionally rotate the projectile to face the target
        
    }

   

    public virtual void OnProjectileHit(GameObject collision)
    {
        if (collision.gameObject.transform == target)
        {
           
            collision.gameObject.TryGetComponent(out IDamageable damageable);
            if (damage != 0)
            {
                damageable?.TakeDamage(damage);

            }
            // Destroy the projectile upon collision
            Destroy(gameObject);
        }
      
    }
   
}

