using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField]
    private GameObject cuttingKnifePrefab;
    [SerializeField]
    private float jitterAmount = 3.5f;
    [SerializeField]
    private float impulseAmount = 2.5f;
    [SerializeField]
    private float constantImpulseStrength = 1f;

    //private Rigidbody2D rb;

    public bool isDragging = false;

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //if (isDragging)
        //{
        //    transform.position += (Vector3)Random.insideUnitCircle * jitterAmount;
        //}

        transform.position += (Vector3)Random.insideUnitCircle * jitterAmount;

        //ApplyImpulse(constantImpulseStrength);


    }

    //void ApplyImpulse(float impulseAmount)
    //{
    //    // Calculate impulse direction 
    //    Vector2 impulseDirection = Random.insideUnitCircle.normalized;
    //    //Vector2 impulseDirection = new Vector2(1.0f, 0f);
    //    Vector2 forcePoint = rb.position;

    //    rb.AddForceAtPosition(impulseDirection * impulseAmount, forcePoint, ForceMode2D.Impulse);
    //}

}
