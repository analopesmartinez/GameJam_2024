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


    private float clock;
    //public bool 

    private void Start()
    {
        clock = 0.0f;
        scale = 1.0f;
    }
    void Update()
    {
        if(valueChanging)
        {
        incrementClock();

        }
        
        // To go to the center, I need to increment/decrement the output of
        // the noise value itself

        // Get a Perlin noise value (between 0 and 1)
        //if (valueChanging)
        //{
        //}

        // Probably put inside 'incrementClock()'
        //Calculate level
        level = Mathf.Clamp(Mathf.FloorToInt(noise * 5),0,4);

        //if (!valueChanging)
        //{
        //    goToCenter(level, noise);
        //}
    }


    void incrementClock()
    {
        float x = clock * scale;
        float y = scale;
        clock += Time.deltaTime;
        noise = Mathf.Clamp(1.2f*Mathf.PerlinNoise(x, y),0f,1f);
    }
    void goToCenter(float currentLevel, float currentNoise)
    {
        float tagretPosition = (0.2f * currentLevel) + 0.1f;
        //float currentPosition = noise;


        float noise = Mathf.Lerp(currentNoise, tagretPosition, Time.deltaTime);
    }
}