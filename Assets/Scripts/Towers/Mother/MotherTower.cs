using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherTower : Tower
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] AbilityBar abilityBar;
    [SerializeField] GameObject beamObject;
    bool isAttacking;
    float timePassed = 0f;
    float timeCoolDown = 0f;
    float attackInterval = 1f; // Attack every 1 second
    float lastAttackTime = 0f;
    float attackDuration = 5f; // Example attack duration
    [SerializeField] Transform boobiesPosition;
    float spriteWidth;

    void Start()
    {
        projectile = projectilePrefab;
        AttackCooldown = 3f;
        canAttack = true;

        ability.requrieProjectileCount = 10;
        if (beamObject != null)
        {
            beamObject = Instantiate(beamObject, transform.position, Quaternion.identity);
            beamObject.SetActive(false); // Hide initially
             spriteWidth = beamObject.GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    public override void Update()
    {
        base.Update();
        if (!canAttack)
        {
            timeCoolDown += Time.deltaTime;
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
            beamObject.SetActive(false); // Hide the beam
            return; // Stop attacking after duration ends
        }

        timePassed += Time.deltaTime;
        UpdateBeam();
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
             // Update the beam's position and rotation
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

    private void UpdateBeam()
    {
        if (beamObject != null)
        {// Activate the beam
            beamObject.SetActive(true);

            // Get the start (Tower) and end (Enemy) positions
            Vector3 start = boobiesPosition.position; // Tower position
            Vector3 end = targetTransform.position; // Enemy position

            // Position the beam at the tower's position
            beamObject.transform.position = start;

            // Calculate direction and rotate the beam to face the enemy
            Vector2 direction = (end - start).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            beamObject.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Calculate the correct beam length
            float distance = Vector2.Distance(start, end);

            // Adjust scale **only on X-axis** while keeping Y unchanged
            Vector3 newScale = beamObject.transform.localScale;
            newScale.x = distance*0.15f;
            beamObject.transform.localScale = newScale;

        }
    }
}
