using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHandler : SingletonMonobehaviour<KnifeHandler>
{
    public GameObject knifePrefab; // Assign in Inspector
    public GameObject cuttingKnifePrefab;
    private GameObject currentKnifeInstance;
    public GameObject tomatoParticlePrefab;
    public bool isKnifeActive;
    public Sprite cutTomatoSprite;

    //public void HandleKnifeClick()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        // Your raycasting and knife click logic
    //    }
    //}

    private void Start()
    {
        isKnifeActive = false;
    }

    private void Update()
    {
        if (isKnifeActive)
        {
            FollowMouseCursor();
        }

        //Knife knife = currentKnifeInstance.gameObject.GetComponent<Knife>();
        //if (knife != null)
        //{
        //    Debug.Log("KNIFE IS DRAGGING");
        //    knife.isDragging = true;
        //}
    }
    public void ActivateKnife()
    {
        isKnifeActive = true;
        //Debug.Log("Knife is now active");
        //Debug.Log(isKnifeActive);
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
            //
            hit.collider.gameObject.GetComponent<MovableObject>().isKnife = true;

            Destroy(currentKnifeInstance);
            currentKnifeInstance = Instantiate(cuttingKnifePrefab, Vector3.zero, Quaternion.identity);

            Animator knifeAnimator = currentKnifeInstance.GetComponentInChildren<Animator>();

            if (knifeAnimator != null)
            {
                knifeAnimator.SetBool("isCutting", true); 

                // Optional: Reset the animation state to allow restarting
                knifeAnimator.Play("Cutting", -1, 0f);
                knifeAnimator.SetBool("isCutting", false);
            }

            // Particle Effects
            GameObject particleEffect = Instantiate(tomatoParticlePrefab, hit.point, Quaternion.identity);
            ParticleSystem ps = particleEffect.GetComponent<ParticleSystem>();

            if (ps != null)
            {
                ps.Play();
                //float totalDuration = ps.main.duration + ps.main.startDelay.constantMax;
                //Destroy(particleEffect, totalDuration);
            }



            // Change tag so that the game knows when everything is cut
            hit.collider.gameObject.tag = "CutTomato";

            // Trigger cut animation or change sprite
            SpriteRenderer spriteRenderer = hit.collider.gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = cutTomatoSprite;
      
            // Example: hit.collider.gameObject.GetComponent<Tomato>().Cut();
        }
    }

}
