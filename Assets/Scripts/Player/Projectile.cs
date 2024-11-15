using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity;
        if (collision.transform.parent.TryGetComponent(out entity))
        {
            entity.TakeDamage(damage, transform.position);
        }

        Destroy(gameObject);
    }
}