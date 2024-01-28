using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public float turnSpeed = 200.0f;

    // private Animator animator;
    private float inputVertical;
    private float inputHorizontal;
    public Rigidbody body;
    public float force;

    private laughterValue laughterScript;

    private void Start()
    {
        GameObject gameObjectWithScriptA = GameObject.Find("laughterValue");
        if (gameObjectWithScriptA != null)
        {
            laughterScript = gameObjectWithScriptA.GetComponent<laughterValue>();
        }
    }

    private void FixedUpdate()
    {
        inputVertical = Input.GetAxis("Vertical");
        body.AddForce(transform.forward * inputVertical * moveSpeed * Time.deltaTime);
    }
    
    
    void Update()
    {
        //Debug.Log(laughterScript.noise);
        //body.AddForce(Vector3.up * force);
        // Get input from keyboard
        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");

        // Move the character forward or backward
        //transform.Translate(Vector3.forward * inputVertical * moveSpeed * Time.deltaTime);


        // Rotate the character left or right
        transform.Rotate(Vector3.up, inputHorizontal * turnSpeed * Time.deltaTime * (2 * (laughterScript.level + 1)));


        
        // Set the Animator parameter to control the walk animation
        // animator.SetFloat("Speed", Mathf.Abs(inputVertical));
    }
    
}