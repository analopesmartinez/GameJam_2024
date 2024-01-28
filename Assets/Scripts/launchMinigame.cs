using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class launchMinigame : MonoBehaviour
{

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("TRIGGERED");

            SceneManager.LoadScene("CookingUpdated");

        }

    }
}
