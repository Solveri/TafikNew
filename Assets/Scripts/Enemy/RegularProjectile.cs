using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularProjectile : Projectile
{

    public override void Update()
    {
        base.Update();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnProjectileHit(collision.gameObject);
    }
}
