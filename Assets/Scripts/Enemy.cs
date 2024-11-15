using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 1f;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
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

    void FixedUpdate()
    {
        if (player != null && rb != null)
        {
            Vector2 direction = ((Vector2)player.position - rb.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }
}
