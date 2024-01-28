using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laughterValue : MonoBehaviour
{
    //public float sineValue;
    public float scale;
    public float noise;
    public int level;
    public bool valueChanging = true;

    private void Start()
    {
        scale = 1.0f;
    }
    void Update()
    {
        // Update sineValue each frame to be the sine of the elapsed time
        //sineValue = Mathf.Sin(Time.time);

        float x = Time.time * scale;
        float y = scale;

        // Get a Perlin noise value (between 0 and 1)
        if (valueChanging)
        {
        noise = Mathf.Clamp(1.5f*Mathf.PerlinNoise(x, y),0f,1f);
        }
        level = Mathf.Clamp(Mathf.FloorToInt(noise * 5),0,4);

        if (!valueChanging)
        {
            goToCenter(level, noise);
        }
    }

    void goToCenter(float currentLevel, float currentNoise)
    {
        float tagretPosition = (0.2f * currentLevel) + 0.1f;
        //float currentPosition = noise;


        float noise = Mathf.Lerp(currentNoise, tagretPosition, Time.deltaTime);
    }
}