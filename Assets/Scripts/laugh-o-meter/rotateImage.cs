using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotateImage : MonoBehaviour
{
    // For rotating the 2D handle sprite
    private RectTransform rectTransform;
    // Reference to the main laughterValue script
    private laughterValue script_laughterValue;

    // Hard coded rotations array corresponding to Laugh-o-meter sprite
    private float[] rotationsArray = new float[5];
    private float currentRotation;

    // Speed at which the handle will move between levels
    [SerializeField] private float levelChange_lerpSpeed = 0.02f;


    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        // Populate rotations array (change for adjusting based on sprite)
        rotationsArray[0] = 1;
        rotationsArray[1] = -45;
        rotationsArray[2] = -90;
        rotationsArray[3] = -130;
        rotationsArray[4] = -175;

        GameObject gameObjectWithScriptA = GameObject.Find("laughterValue");
        if (gameObjectWithScriptA != null)
        {
            script_laughterValue = gameObjectWithScriptA.GetComponent<laughterValue>();
        }
        else
        {
            Debug.LogError("Could not find reference to laughterValue script!");
            Debug.Break();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // To further improve this, need to implement a "smoothLevel" into the laughterValue script
        // which can then be used to smoothly ramp up and down the offset intensity, based on
        // how high the laughter level is.


        float shake_timeOffset = Mathf.Clamp(Mathf.PerlinNoise1D(Time.time)+1,1f,(1.5f))*(10*script_laughterValue.noise);
        // Rotation offset based on noise
        float noiseOffset = (Mathf.PerlinNoise1D(Time.time+shake_timeOffset) * 30 - 15);
        // Rotation offset based on time
        float sineOffset = Mathf.Sin(Time.fixedTime+40) * 10;

        // Get the target rotation from rotationsArray
        float targetRotation = rotationsArray[script_laughterValue.level];

        // Perform the interpolation
        currentRotation = Mathf.Lerp(currentRotation, targetRotation, levelChange_lerpSpeed);

        rectTransform.rotation = Quaternion.Euler(0, 0, currentRotation+noiseOffset+sineOffset);
        
    }
}
