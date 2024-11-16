using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnItem
{
    public GameObject item;
    public int maxSpawn;
}

[Serializable]
public class SpawnArea
{
    public SpriteRenderer spawnArea;
    public int maxSpawn;
    [HideInInspector] public List<SpawnItem> objectsToSpawn;
}

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnItem> spawnItems;
    [SerializeField] private List<SpawnArea> spawnAreas;
    [SerializeField] private float minSpawnBounds = 0.5f;
    private void Start()
    {
        AssignItemsToSpawnAreas();
        SpawnItems();
    }

    private void AssignItemsToSpawnAreas()
    {
        int spawnAreasCount = spawnAreas.Count;
        List<bool> isAreaFull = new List<bool>(new bool[spawnAreasCount]);

        foreach (SpawnItem item in spawnItems)
        {
            int spawnCount = item.maxSpawn;
            while (spawnCount-- > 0)
            {
                int maxTry = 5;
                int areaToAssign = UnityEngine.Random.Range(0, spawnAreasCount);

                while (isAreaFull[areaToAssign] && maxTry > 0)
                {
                    areaToAssign = UnityEngine.Random.Range(0, spawnAreasCount);
                    maxTry--;
                }

                if (maxTry == 0)
                {
                    break;
                }

                SpawnArea area = spawnAreas[areaToAssign];
                area.objectsToSpawn.Add(item);
                if (area.maxSpawn == area.objectsToSpawn.Count)
                {
                    isAreaFull[areaToAssign] = true;
                }
            }
        }
    }

    private void SpawnItems()
    {
        foreach (SpawnArea area in spawnAreas)
        {
            List<Vector3> usedPositions = new();
            Bounds bounds = area.spawnArea.bounds;

            foreach (SpawnItem item in area.objectsToSpawn)
            {
                Vector3 spawnPosition;
                int maxAttempts = 10;
                bool isValidPositionFound = false;

                do
                {
                    float randomX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
                    float randomY = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
                    spawnPosition = new Vector3(randomX, randomY, 0);

                    isValidPositionFound = true;
                    foreach (Vector3 pos in usedPositions)
                    {
                        if (Vector3.Distance(pos, spawnPosition) < minSpawnBounds)
                        {
                            isValidPositionFound = false;
                            break;
                        }
                    }

                    maxAttempts--;

                } while (!isValidPositionFound || maxAttempts > 0);


                if (isValidPositionFound)
                {
                    usedPositions.Add(spawnPosition);
                    Instantiate(item.item, spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}
