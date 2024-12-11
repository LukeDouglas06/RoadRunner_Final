using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI Title;
    public bool gameStarted = false; // To track if the game has started
    public SpawnManager spawnManager;
    public GameObject titleScreen;
    public float defaultVehicleSpeed = 10f; // Default vehicle speed
    public Button restartButton;
    public PlayerController PlayerController;


    void Start()
    {
        Time.timeScale = 0; // Pause the game initially
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        if (spawnManager != null)
        {
            spawnManager.StopSpawning();
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerController.gravityModifier = 1.7f;
    }

    public void StartGame(int difficulty)
    {
        gameStarted = true;
        Time.timeScale = 1; // Resume the game

        float adjustedSpeed = defaultVehicleSpeed * difficulty; // Adjust speed based on difficulty
        if (spawnManager != null)
        {
            spawnManager.SetVehicleSpeed(adjustedSpeed); // Pass adjusted speed to SpawnManager
        }

        titleScreen.gameObject.SetActive(false);
    }


    private void UpdateVehicleSpeeds(float newSpeed)
    {
        VehicleMovement[] vehicles = FindObjectsOfType<VehicleMovement>();
        foreach (VehicleMovement vehicle in vehicles)
        {
            vehicle.UpdateMoveSpeed(newSpeed);
        }
    }
}
