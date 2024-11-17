using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigarettePickup : MonoBehaviour
{
    SanityController sanityController;

    private void Start()
    {
        sanityController = GameObject.FindGameObjectWithTag("Player").GetComponent<SanityController>();
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sanityController.AddSanity(10);
            Destroy(gameObject);
        }
    }

}
