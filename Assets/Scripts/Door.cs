using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject closed;
    [SerializeField] private GameObject opened;

    public bool Opened { get; private set; }

    private void Start()
    {
        Close();
    }

    public void Open()
    {
        Opened = true;
        closed.SetActive(false);
        opened.SetActive(true);
    }

    public void Close()
    {
        Opened = false;
        closed.SetActive(true);
        opened.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !collision.isTrigger) // temporary testing func before we figure out inventory and interactions
        {
            Debug.Log(collision.gameObject.name);

            if (Opened)
                Close();
            else
                Open();
        }
    }
}
