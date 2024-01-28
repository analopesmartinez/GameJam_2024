using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField]
    private GameObject cuttingKnifePrefab;
    [SerializeField]
    private float jitterAmount = 0.1f;
    [SerializeField]
    private float impulseAmount = 2.5f;
    [SerializeField]
    private float constantImpulseStrength = 1f;

    public bool isDragging = false;

    private void Update()
    {
        if (isDragging)
        {
            transform.position += (Vector3)Random.insideUnitCircle * jitterAmount;
        }
    }

}
