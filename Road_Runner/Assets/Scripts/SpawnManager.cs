using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] vehiclePrefabs; // Array of vehicle prefabs to spawn
    private float spawnRangeX = 8; // Range for spawning vehicles on the X-axis
    private float startDelay = 2; // Delay before the first vehicle spawns
    private float spawnInterval = 1; // Time interval between each spawn
    private PlayerController playerControllerScript; // Reference to the PlayerController to check if the game is over
    private bool shouldStopSpawning = false; // Flag to stop vehicle spawning
    private float vehicleSpeed = 10f; // Default vehicle speed

    private float spawnRangeZ = 40;

    void Start()
    {
        // Initialize references and start vehicle spawning
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnRandomVehicle", startDelay, spawnInterval);
    }

    // Method to spawn a random vehicle at a random position within the defined range
    void SpawnRandomVehicle()
    {
        if (!playerControllerScript.gameOver && !shouldStopSpawning)
        {
            int vehicleIndex = Random.Range(0, vehiclePrefabs.Length); // Randomly select a vehicle prefab
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnRangeZ); // Random spawn position

            // Instantiate the selected vehicle and set its movement speed
            GameObject vehicle = Instantiate(vehiclePrefabs[vehicleIndex], spawnPos, vehiclePrefabs[vehicleIndex].transform.rotation);
            vehicle.GetComponent<VehicleMovement>().UpdateMoveSpeed(vehicleSpeed); // Set speed on vehicle
        }
    }

    // Method to stop vehicle spawning (e.g., on game over)
    public void StopSpawning()
    {
        shouldStopSpawning = true;
        CancelInvoke("SpawnRandomVehicle"); // Stop invoking the spawn method
    }

    // Method to update the vehicle speed
    public void SetVehicleSpeed(float newSpeed)
    {
        vehicleSpeed = newSpeed; // Update the speed for new vehicles that spawn
    }
}
