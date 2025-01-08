using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // Speed of the projectile
    [SerializeField] private float lifetime = 5f; // Time before the projectile is destroyed
    private Transform target; // The target the projectile will move toward

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Start()
    {
        // Destroy the projectile after a set time if it doesn’t hit anything
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
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

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collision logic here
        if (collision.gameObject.transform == target)
        {
            Debug.Log("Projectile hit the target: " + target.name);
            collision.gameObject.TryGetComponent(out IDamageable damageable);
            damageable?.TakeDamage(1);
        }
        else
        {
            Debug.Log("Projectile hit: " + collision.gameObject.name);
        }

        // Destroy the projectile upon collision
        Destroy(gameObject);
    }
}

