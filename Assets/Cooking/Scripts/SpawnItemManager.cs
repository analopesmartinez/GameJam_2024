using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemManager : SingletonMonobehaviour<SpawnItemManager>
{
    // DON'T FORGET THE OFFSET IN THE POSITIONS
    public GameObject arrowPrefab;
    public GameObject[] objectPrefabs;
    public Transform spawnObjectPositionTransform;
    public Vector3[] spawnArrowPositions;
    public int instructionIndex = 0;
    private GameObject currentArrowPrefab;
    public GameObject knifeInScene;

    public bool isKnife = false;

    private void Start()
    {
        knifeInScene = GameObject.FindGameObjectWithTag("KnifeInScene");
        InstantiateObjPrefab(instructionIndex);
        InstantiateArrowPrefab(instructionIndex);
        
  
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && objectPrefabs[instructionIndex].tag == "Knife")
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit the specific object
                if (hit.collider.gameObject == knifeInScene)
                {
                    //Debug.Log("HIT");
                    HandleKnifeInSceneClick();
                    isKnife = true;
                    //InstantiateObjPrefab(instructionIndex);
                }
            }
         
        }
    }

    private void HandleKnifeInSceneClick()
    {
        // Delete or deactivate the knife and arrow in the scene
        Destroy(knifeInScene);
        Destroy(currentArrowPrefab);

        // Activate the knife handling logic
        KnifeHandler.Instance.ActivateKnife();
    }

    void InstantiateObjPrefab(int index)
    {
        if (objectPrefabs[index] != null)
        {
            if (objectPrefabs[index].tag == "Knife")
            {
                // ALL INGREDIENTS NOW ON CHOPPING BOARD
             
                if (isKnife)
                {
                    //Debug.Log("Clicked On Knife");
                    //Destroy(knifeInScene);
                    //Destroy(currentArrowPrefab);
                    //Instantiate(objectPrefabs[index], spawnObjectPositionTransform.position, Quaternion.identity);
                    //InstantiateArrowPrefab(instructionIndex);
                    //instructionIndex++;
                    //isKnife = false;
                }
            }
            else
            {
                Instantiate(objectPrefabs[index], spawnObjectPositionTransform.position, Quaternion.identity);

            }
        }
        else
        {
            Debug.Log("No Object at index 0 in spawn objects array");
        }

    }

    void InstantiateArrowPrefab(int index)
    {
        if (spawnArrowPositions[index] != null)
        {
            currentArrowPrefab = Instantiate(arrowPrefab, spawnArrowPositions[index], Quaternion.identity);
        }
        else
        {
            //Debug.Log("No next arrow positions");
        }
    }

    public void RespawnObject(int prefabIndex)
    {
        if (prefabIndex >= 0 && prefabIndex < objectPrefabs.Length)
        {
            Instantiate(objectPrefabs[prefabIndex], spawnObjectPositionTransform.position, Quaternion.identity);
        }
    }

    public void DestroyArrowObjectAndLoadNext()
    {
        Destroy(currentArrowPrefab);
        instructionIndex++;
        InstantiateObjPrefab(instructionIndex);
        InstantiateArrowPrefab(instructionIndex);
    }



   
}
