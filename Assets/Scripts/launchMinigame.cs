using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class launchMinigame : MonoBehaviour
{
    private bool launched = false;
    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (launched == false)
        {
            launched = true;
            if (other.CompareTag("Player"))
            SceneManager.LoadScene("CookingUpdated");
        }
        
        
    }
}
