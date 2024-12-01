using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public int initHealth = 10;
    public float moveSpeed = 1f;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        Health = initHealth;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("no rb");
        }

    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(5, collision.transform.position);
        }


      
    }

    private void OnEnable()
    {
        Game.currentEnemyCount++;
    }

    private void OnDisable()
    {
        Game.currentEnemyCount--;
    }

    void FixedUpdate()
    {
        if (player != null && rb != null)
        {
            Vector2 direction = ((Vector2)player.position - rb.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }

    public override void TakeDamage(int dmg, Vector2 dirFrom)
    {
        Health -= dmg;
        if (Health <= 0)
        { 
            Destroy(this.gameObject);
        }
    }
}
