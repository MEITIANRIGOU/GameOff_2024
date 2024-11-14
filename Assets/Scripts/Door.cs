using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector2 size => GetComponent<SpriteRenderer>().bounds.size;

    public Action OnInteract;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnInteract?.Invoke();
    }
}
