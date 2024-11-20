using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    FlashlightController flashlightController;
    InventorySystem inventorySystem;

    private void Start()
    {
        flashlightController = GameObject.FindGameObjectWithTag("Player").GetComponent<FlashlightController>();
        inventorySystem = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           //flashlightController.AddPower(100);
            inventorySystem.battAmount++;
            Destroy(gameObject);
        }
    }
}
