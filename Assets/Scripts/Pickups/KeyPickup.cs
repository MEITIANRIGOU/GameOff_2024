using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    InventorySystem inventorySystem;

    private void Start()
    {
        inventorySystem = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inventorySystem.keyAmount++;
            Destroy(gameObject);
        }
    }
}
