using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCubes : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    [SerializeField] int SpawnCount = 5;

    [SerializeField] float minX = -10f;
    [SerializeField] float maxX = 10f;
    [SerializeField] float minY = 0f;
    [SerializeField] float maxY = 10f;
    [SerializeField] float minZ = -10f;
    [SerializeField] float maxZ = 10f;

    private int currentObjectCount;

    void Start()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            SpawnObject();
        }
    }

    public void SpawnObject()
    {
        Instantiate(objectPrefab, RandomSpawnPosition(), transform.rotation);
        currentObjectCount++;
    }

    Vector3 RandomSpawnPosition()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        float z = Random.Range(minZ, maxZ);
        return new Vector3(x, y, z);
    }

    public void ReplaceObject()
    {
        currentObjectCount--;
        SpawnObject();
    }
}