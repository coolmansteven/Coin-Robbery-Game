using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;  // Reference to the Coin prefab
    public int coinCount = 5;      // Number of coins to spawn
    public float spawnRangeX = 8f; // Horizontal range for spawning
    public float spawnRangeY = 4f; // Vertical range for spawning
    public float spawnInterval = 3f;  // Interval time between spawns

    void Start()
    {
        // Initial spawn of coins
        SpawnCoins();
        // Start spawning coins at regular intervals
        InvokeRepeating(nameof(SpawnCoins), spawnInterval, spawnInterval);
    }

    void SpawnCoins()
    {
        for (int i = 0; i < coinCount; i++)
        {
            // Generate a random position within the defined range
            float xPosition = Random.Range(-spawnRangeX, spawnRangeX);
            float yPosition = Random.Range(-spawnRangeY, spawnRangeY);
            Vector3 spawnPosition = new Vector3(xPosition, yPosition, 0);

            // Spawn a new coin instance at the random position
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
