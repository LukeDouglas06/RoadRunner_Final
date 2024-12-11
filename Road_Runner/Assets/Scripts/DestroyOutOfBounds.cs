using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{

    private float lowerBound = -15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if an object goes past the view of the player game over
        if(transform.position.z < lowerBound)
        {
            Destroy(gameObject);
        }
    }
}

