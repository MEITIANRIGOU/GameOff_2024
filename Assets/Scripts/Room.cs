using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Vector2Int size;
    public Vector2Int Size => size;

    [SerializeField] private Door prefabDoorFront;
    [SerializeField] private Door prefabDoorSide;

    public Action<Direction> OnExit;

    public void SpawnDoors(bool north, bool south, bool west, bool east)
    {
        float z = -0.1f;

        if (north)
        {
            GameObject door = Instantiate(prefabDoorFront.gameObject, transform);
            door.transform.position = new Vector3(0, size.y / 2f, 0);
            door.transform.position += new Vector3(0, 1, 0);
            door.GetComponent<Door>().OnInteract += delegate { DoorInteract(Direction.North); };
        }
        if (south)
        {
            GameObject door = Instantiate(prefabDoorFront.gameObject, transform);
            door.transform.position = new Vector3(0, -size.y / 2f, 0);
            door.transform.position -= new Vector3(0, 1, 0);
            door.GetComponent<Door>().OnInteract += delegate { DoorInteract(Direction.South); };
        }
        if (west)
        {
            GameObject door = Instantiate(prefabDoorSide.gameObject, transform);
            door.transform.position = new Vector3(-size.x / 2f, 0, 0);
            door.GetComponent<Door>().OnInteract += delegate { DoorInteract(Direction.West); };
        }
        if (east)
        {
            GameObject door = Instantiate(prefabDoorSide.gameObject, transform);
            door.transform.position = new Vector3(size.x / 2f, 0, 0);
            door.GetComponent<Door>().OnInteract += delegate { DoorInteract(Direction.East); };
        }
    }

    void DoorInteract(Direction dir) 
    {
        OnExit?.Invoke(dir);
    }
}
