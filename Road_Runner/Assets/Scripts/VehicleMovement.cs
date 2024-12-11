using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public float moveSpeed = 10f; 
    private Rigidbody rb;
    private PlayerController playerControllerScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        if (!playerControllerScript.gameOver)
        {
            Debug.Log(moveSpeed);
            rb.velocity = new Vector3(0, rb.velocity.y, -moveSpeed);
        }
        else
        {
            rb.velocity = Vector3.zero; // Stop movement when game over
        }

    }

    public void UpdateMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}
