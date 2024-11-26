using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoor : MonoBehaviour
{

    public GameObject darkness, darkness2;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           darkness.SetActive(false);
        }
    }

    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            darkness.SetActive(true);
        }
    }
}
