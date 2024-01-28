using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushHandler : SingletonMonobehaviour<BrushHandler>
{
    public GameObject brushPrefab; // Assign in Inspector
    private GameObject currentBrushInstance;
    public GameObject brushParticlePrefab;
    public bool isBrushActive;
    public Sprite noSprite;
    //public Sprite cutTomatoSprite;

    //public void HandleKnifeClick()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        // Your raycasting and knife click logic
    //    }
    //}

    private void Start()
    {
        isBrushActive = false;
    }

    private void Update()
    {
        if (isBrushActive)
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
    public void ActivateBrush()
    {
        isBrushActive = true;
        //Debug.Log("Knife is now active");
        //Debug.Log(isKnifeActive);
        currentBrushInstance = Instantiate(brushPrefab, Vector3.zero, Quaternion.identity);
        
        //currentKnifeInstance.GetComponent<Knife>().isDragging = true;
    }

    private void FollowMouseCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the knife stays in the 2D plane
        if (currentBrushInstance != null)
        {
            currentBrushInstance.GetComponentInChildren<Transform>().position = mousePosition;
        }

        // Check for a click to initiate a cut
        if (Input.GetMouseButtonDown(0))
        {
            TryBrush();
        }
    }

    private void TryBrush()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        Debug.Log(hit.point);

        if (hit.collider != null && hit.collider.CompareTag("Dirt"))
        {
            //
            //hit.collider.gameObject.GetComponent<MovableObject>().isKnife = true;

            //Destroy(currentKnifeInstance);
            //currentKnifeInstance = Instantiate(cuttingKnifePrefab, Vector3.zero, Quaternion.identity);

            Animator knifeAnimator = currentBrushInstance.GetComponentInChildren<Animator>();

            if (knifeAnimator != null)
            {
                knifeAnimator.SetBool("isCutting", true); 

                // Optional: Reset the animation state to allow restarting
                knifeAnimator.Play("Cutting", -1, 0f);
                knifeAnimator.SetBool("isCutting", false);
            }

            // Particle Effects
            GameObject particleEffect = Instantiate(brushParticlePrefab, hit.point, Quaternion.identity);
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
            spriteRenderer.sprite = noSprite;
      
            // Example: hit.collider.gameObject.GetComponent<Tomato>().Cut();
        }
    }

}
