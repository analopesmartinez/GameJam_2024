using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laughterValue : MonoBehaviour
{
    //public float sineValue;
    public float scale;
    public float noise;

    private void Start()
    {
        scale = 1.0f;
    }
    void Update()
    {
        // Update sineValue each frame to be the sine of the elapsed time
        //sineValue = Mathf.Sin(Time.time);

        float x = Time.time * scale;
        float y = transform.position.y * scale;

        // Get a Perlin noise value (between 0 and 1)
        noise = Mathf.Clamp(1.5f*Mathf.PerlinNoise(x, y),0,1);
    }
}