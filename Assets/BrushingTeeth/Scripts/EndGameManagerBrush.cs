using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManagerBrush : SingletonMonobehaviour<EndGameManagerBrush>
{
    private GameObject unbrushedObject;
    public GameObject particleBrushPrefab;




    private bool isEnding = false;

    void Update()
    {
        unbrushedObject = GameObject.FindGameObjectWithTag("Dirt");

        if (unbrushedObject == null && SpawnItemManagerBrush.Instance.activatedBrush == true && isEnding == false)
        {
            isEnding = true;

            //Application.Quit();

            StartCoroutine(SpawnParticlesOverTime(1f, 0.01f)); 
            // where do i put this if I want it to run for three seconds 


            // Randomly instantiate particles

            // wait for a while

            // Exit game 


        }
    }

    IEnumerator SpawnParticlesOverTime(float duration, float spawnInterval)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            RandomInstantiateParticles(); // Instantiate a particle
            yield return new WaitForSeconds(spawnInterval); // Wait for the next spawn
            timeElapsed += spawnInterval;
        }

        Debug.Log("ENDING GAME");
        //EndMinigame();
        SceneManager.LoadScene("Ana's scene");
        UnloadCurrentScene();
    }

    private void UnloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(currentSceneName);
    }

    //private void EndMinigame()
    //{
    //    // Assuming you know the name of the minigame scene
    //    string minigameSceneName = "Cooking";
    //    SceneManager.UnloadSceneAsync(minigameSceneName);

    //    // Alternatively, if you want to unload the currently active scene:
    //    // SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    //}


    //private void EndGame()
    //{
    //    Application.Quit();
    //}

    private void RandomInstantiateParticles()
    {
        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0); 
        Instantiate(particleBrushPrefab, randomPosition, Quaternion.identity);
    }
}
