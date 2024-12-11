using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private PlayerController playerControllerScript;

    void Start()
    {
        // Find and reference the PlayerController script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // This method is called when another object enters the coin's trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with the coin is the player
        if (other.CompareTag("Player"))
        {
            // Increase the player's score
            playerControllerScript.IncreaseScore();

            // Destroy the coin after it is collected
            Destroy(gameObject);
        }
    }
}
