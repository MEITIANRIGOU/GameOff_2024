using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pickup : MonoBehaviour
{
    [SerializeField]
    private PickupItem pickupItem;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var inventory = collision.gameObject.GetComponent<InventorySystem>();
            if (inventory == null) return;

            switch (pickupItem.type)
            {
                case PickupItem.PickupType.Evidence:
                    inventory.AddEvidence(pickupItem);
                    break;
                case PickupItem.PickupType.Battery:
                    inventory.battAmount += pickupItem.amount;
                    break;
                case PickupItem.PickupType.Cigarette:
                    inventory.cigAmount += pickupItem.amount;
                    break;
                case PickupItem.PickupType.Key:
                    inventory.keyAmount += pickupItem.amount;
                    break;
            }

            Destroy(gameObject);
        }
    }
}

