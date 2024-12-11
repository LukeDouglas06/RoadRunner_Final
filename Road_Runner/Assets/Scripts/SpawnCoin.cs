using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    public GameObject[] coinPrefabs;
    private float spawnRangeX = 8;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    private PlayerController playerControllerScript;
    private bool shouldStopSpawning = false; 
    public float minHeight = 3f;
    public float maxHeight = 8f;

    void Start()
    {
        // Change method name here to avoid conflict with the method itself
        InvokeRepeating("SpawnRandomCoin", startDelay, spawnInterval);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }


    void SpawnRandomCoin()
    {
        if (!playerControllerScript.gameOver && !shouldStopSpawning)
        {
            int coinIndex = Random.Range(0, coinPrefabs.Length);

            // Generate a random spawn position with a random height between minHeight and maxHeight
            float randomHeight = Random.Range(minHeight, maxHeight) + 0.5f;
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), randomHeight, spawnRangeX);

            // Instantiate the coin at the random position
            Instantiate(coinPrefabs[coinIndex], spawnPos, coinPrefabs[coinIndex].transform.rotation);
        }
    }

    public void StopSpawning()
    {
        shouldStopSpawning = true;
        CancelInvoke("SpawnRandomCoin"); // Use the new method name here as well
    }
}
