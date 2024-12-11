using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    private PlayerController playerControllerScript;

    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            // Check if the background has moved past its starting position
            if (transform.position.z < startPos.z - repeatWidth)
            {
                transform.position = startPos; // Reset to the starting position
            }
        }
    }
}
