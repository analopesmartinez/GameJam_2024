using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShaker : MonoBehaviour
{
    public float amplitude = 1.0f; // The maximum distance moved along the Z-axis.
    public float frequency = 1.0f; // How fast the object moves up and down.

    private Vector3 startPosition;

    void Start()
    {
        // Store the starting position of the GameObject.
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Z position.
        float z = amplitude * Mathf.Sin(Time.time * frequency);

        // Update the position of the GameObject.
        transform.position = new Vector3(startPosition.x, startPosition.y + z, startPosition.z + z);
    }
}