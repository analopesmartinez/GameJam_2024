using UnityEngine;
using System.Collections;


public class followPlayer : MonoBehaviour
{
    public Transform player; // Player's Transform
    public Vector3 offset = new Vector3(0,3,-5); // Offset distance between the player and camera

    public float smoothTime = 0.3F; // Time for dampening
    private Vector3 velocity = Vector3.zero; // This stores the current velocity, which is modified by SmoothDamp

    void Start()
    {
        offset.Set(0, 3, -5);
    }
    void Update()
    {
        // Define a target position above and behind the player
        Vector3 targetPosition = player.position + offset;

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
        transform.LookAt(player);
    }
}
