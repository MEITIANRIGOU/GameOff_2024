using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static Evidence;

public class NotePickup : MonoBehaviour
{
    [SerializeField]
    private EvidenceItem evidenceItem; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InventorySystem inventory = collision.gameObject.GetComponent<InventorySystem>();
            if (inventory != null)
            {
                inventory.AddEvidence(evidenceItem); 
                Destroy(gameObject); 
            }
        }
    }

}