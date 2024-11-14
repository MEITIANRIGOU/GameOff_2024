using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private Room[] roomsRegular;

    private Room[,] topology;

    private Vector2Int currentPlayerCoordinates;

    // temp
    float cooldown;

    private void Start()
    {
        GenerateLevel();

        EnterRoom(0, 0, Direction.North);
    }

    void GenerateLevel()
    {
        int x = 4;
        int y = 3;

        topology = new Room[x, y];

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                topology[i, j] = Instantiate(roomsRegular[Random.Range(0, roomsRegular.Length)].gameObject, transform).GetComponent<Room>();

                bool north = j < y - 1;
                bool south = j > 0;
                bool west = i > 0;
                bool east = i < x - 1;

                topology[i, j].SpawnDoors(north, south, west, east);
                topology[i, j].OnExit += LeaveRoom;

                topology[i, j].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    void LeaveRoom(Direction dir)
    {
        if (cooldown > 0)
            return;

        switch (dir)
        {
            case Direction.North:
                EnterRoom(currentPlayerCoordinates.x, currentPlayerCoordinates.y + 1, dir);
                break;
            case Direction.South:
                EnterRoom(currentPlayerCoordinates.x, currentPlayerCoordinates.y - 1, dir);
                break;
            case Direction.East:
                EnterRoom(currentPlayerCoordinates.x + 1, currentPlayerCoordinates.y, dir);
                break;
            case Direction.West:
                EnterRoom(currentPlayerCoordinates.x - 1, currentPlayerCoordinates.y, dir);
                break;
        }
    }

    void EnterRoom(int x, int y, Direction dir)
    {
        currentPlayerCoordinates = new Vector2Int(x, y);

        cooldown = 0.5f;

        Room newRoom = topology[x, y];

        for (int i = 0; i < topology.GetLength(0); i++)
        {
            for (int j = 0; j < topology.GetLength(1); j++)
            {
                bool active = topology[i, j] == newRoom;

                topology[i, j].gameObject.SetActive(active);
            }
        }

        Vector2 playerNewPos = new Vector2(0, 0);

        switch (dir)
        {
            case Direction.North:
                playerNewPos = new Vector2(0, -newRoom.Size.y / 2f);
                break;
            case Direction.South:
                playerNewPos = new Vector2(0, newRoom.Size.y / 2f);
                break;
            case Direction.East:
                playerNewPos = new Vector2(-newRoom.Size.x / 2f, 0);
                break;
            case Direction.West:
                playerNewPos = new Vector2(newRoom.Size.x / 2f, 0);
                break;
        }

        player.transform.position = new Vector3(playerNewPos.x, playerNewPos.y, player.transform.position.z);
    }

    void ClearRoom()
    {
        for (int y = 0; y < transform.childCount; y++)
        {
            Destroy(transform.GetChild(y).gameObject);
        }
    }
}

public enum Direction
{
    North,
    South,
    West,
    East
}