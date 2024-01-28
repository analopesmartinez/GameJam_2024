using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHandler : SingletonMonobehaviour<KnifeHandler>
{
    public GameObject knifePrefab; // Assign in Inspector
    public GameObject cuttingKnifePrefab;
    private GameObject currentKnifeInstance;
    private bool isKnifeActive = false;

    //public void HandleKnifeClick()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        // Your raycasting and knife click logic
    //    }
    //}

    private void Update()
    {
        if (isKnifeActive)
        {
            FollowMouseCursor();
        }
    }
    public void ActivateKnife()
    {
        isKnifeActive = true;
        currentKnifeInstance = Instantiate(knifePrefab, Vector3.zero, Quaternion.identity);
        //currentKnifeInstance.GetComponent<Knife>().isDragging = true;
    }

    private void FollowMouseCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the knife stays in the 2D plane
        if (currentKnifeInstance != null)
        {
            currentKnifeInstance.GetComponentInChildren<Transform>().position = mousePosition;
        }

        // Check for a click to initiate a cut
        if (Input.GetMouseButtonDown(0))
        {
            TryCut();
        }
    }

    private void TryCut()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        Debug.Log(hit.point);

        if (hit.collider != null && hit.collider.CompareTag("Tomato"))
        {
            currentKnifeInstance = Instantiate(cuttingKnifePrefab, Vector3.zero, Quaternion.identity);
            
            // Trigger cut animation or change sprite
            // You can also call a method on the Tomato object to handle being cut
            Debug.Log("Tomato cut!");
            // Example: hit.collider.gameObject.GetComponent<Tomato>().Cut();
        }
    }

}
