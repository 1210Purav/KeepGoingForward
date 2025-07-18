using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundGeneration : MonoBehaviour
{
    public GameObject ground;
    public Transform player;
    public int numberOfTiles = 5; // Number of tiles to start with
    public float tileLength = 6.7534f; // Size of each tile
    private float safeZone = 4.1f; // Distance before an old tile is deleted
    private float lastPlayerPosZ;

    private Vector3 spawnPosition = new Vector3(0f, 0f, -4.42f); // Starting spawn position
    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        lastPlayerPosZ = player.transform.position.z;

        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile();
        }
    }

    void FixedUpdate()
    {
        float playerZ = player.transform.position.z;

        if (playerZ > activeTiles[0].transform.position.z + tileLength + safeZone)
        {
            SpawnTile();
            DeleteTile();
            lastPlayerPosZ = playerZ;
        }
    }

    void SpawnTile()
    {
        GameObject newTile = Instantiate(ground, spawnPosition, Quaternion.identity);
        activeTiles.Add(newTile);
        spawnPosition += Vector3.forward * tileLength;
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
