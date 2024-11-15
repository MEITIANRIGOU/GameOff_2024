using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int Health { get; protected set; }

    public abstract void TakeDamage(int dmg, Vector2 dirFrom);
}
