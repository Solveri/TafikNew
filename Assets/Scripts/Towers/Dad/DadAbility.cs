using UnityEngine;

public class DadAbility : Ability
{
    [SerializeField] Projectile projectile;

    public override void Activate(GameObject d)
    {
        Projectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
       
        int randomValue = UnityEngine.Random.Range(5, 9); // Random integer between 5 and 8
        Debug.Log("Shush"+randomValue);
        newProjectile.SetTarget(d.transform, randomValue);
    }

    void Update()
    {

    }
}
