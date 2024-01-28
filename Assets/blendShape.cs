using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blendShape : MonoBehaviour
{
    private SkinnedMeshRenderer renderer;
    private laughterValue laughterScript;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SkinnedMeshRenderer>();
        GameObject gameObjectWithScriptA = GameObject.Find("laughterValue");
        if (gameObjectWithScriptA != null)
        {
            laughterScript = gameObjectWithScriptA.GetComponent<laughterValue>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        renderer.SetBlendShapeWeight(0, (1+Mathf.Sin(Time.time*20f * (laughterScript.noise*100f)) *200 * laughterScript.noise));
        renderer.SetBlendShapeWeight(1, (1+Mathf.Sin(Time.time*20f * (laughterScript.noise *100f)) *200 * laughterScript.noise));
        //renderer.SetBlendShapeWeight(1, (1 + Mathf.Cos(Time.time * 0.5f) * 100));
        //renderer.SetBlendShapeWeight(2, Mathf.sin(Time.deltaTime * 0.5f));
    }
}
