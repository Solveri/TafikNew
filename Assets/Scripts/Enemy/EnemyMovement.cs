using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Vector2 Dir;
    [SerializeField] Rigidbody2D rb;
   [SerializeField] int currentPathIndex = 0;
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = LevelManager.instance.PathPoints[currentPathIndex];
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.3f)
        {
            currentPathIndex++;
            if (currentPathIndex < LevelManager.instance.PathPoints.Length)
                target = LevelManager.instance.PathPoints[currentPathIndex];
            if (currentPathIndex == LevelManager.instance.PathPoints.Length)
            {

                PlayerManager.instance.TakeDamage(1f);
                CoinsManager.instance.AddCoins(15);
                Destroy(gameObject);
                return;
            }
        }

    }
    private void OnDestroy()
    {
        CoinsManager.instance.AddCoins(1);
    }
    private void FixedUpdate()
    {
        Vector2 Direction = (target.position - transform.position).normalized;

        rb.velocity = Direction * moveSpeed * Time.deltaTime;
    }


}

