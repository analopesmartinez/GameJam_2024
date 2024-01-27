using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotateImage : MonoBehaviour
{

    public float rotationValue;
    private RectTransform rectTransform;
    private laughterValue scriptAInstance;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        GameObject gameObjectWithScriptA = GameObject.Find("laughterValue");
        if (gameObjectWithScriptA != null)
        {
            scriptAInstance = gameObjectWithScriptA.GetComponent<laughterValue>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (scriptAInstance != null)
        {
            // Use the rotationValue from ScriptA
            rectTransform.rotation = Quaternion.Euler(0, 0, -scriptAInstance.noise*180);
            Debug.Log(scriptAInstance.noise);
        }

        //rectTransform.rotation = Quaternion.Euler(0,0,rotationValue);
    }
}
