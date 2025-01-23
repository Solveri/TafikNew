using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DadProjectile : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    private void Explosion(GameObject originalTarget)
    {
        var hits = Physics2D.CircleCastAll(this.transform.position, 0.5f, Vector2.right);
        Debug.Log(hits.Length);
        foreach (var hit in hits)
        {
            if (hit.transform.gameObject == originalTarget)
            {
                continue;
            }
            else if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Projectile hit the target: " + target.name);
                hit.transform.gameObject.TryGetComponent(out IDamageable damageable);
                if (damage != 0)
                {
                    
                    damageable?.TakeDamage(damage);

                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 0.5f);
    }
    public override void OnProjectileHit(GameObject collision)
    {
        base.OnProjectileHit(collision);
        
        Explosion(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnProjectileHit(collision.gameObject);
    }
}
