using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class audioSelect : MonoBehaviour
{
    private laughterValue laughterScript;
    private int lastValue;
    private int currentValue;

    //private Dictionary<int, List<string>> dict;
    //Dictionary<int, List<int>> stateAudioDict = new Dictionary<int, List<int>>();
    Dictionary<int, int[]> stateAudioDict = new Dictionary<int, int[]>();


    [SerializeField] AudioClip[] clips; // drag and add audio clips in the inspector
    AudioSource audioSource;
    void Start()
    {
        stateAudioDict.Add(0, new int[] { 0, 8 });
        stateAudioDict.Add(1, new int[] { 9, 15 });
        stateAudioDict.Add(2, new int[] { 16, 20 });
        stateAudioDict.Add(3, new int[] { 21, 25 });
        stateAudioDict.Add(4, new int[] { 26, 32 });

        audioSource = GetComponent<AudioSource>();

        GameObject gameObjectWithScriptA = GameObject.Find("laughterValue");
        if (gameObjectWithScriptA != null)
        {
            laughterScript = gameObjectWithScriptA.GetComponent<laughterValue>();
        }

        currentValue = laughterScript.level;
        ChangeTheSound(laughterScript.level);
        laughterScript.valueChanging = false;

    }

    private void Update()
    {
        currentValue = laughterScript.level;

        if (!audioSource.isPlaying)
        {
            //Debug.Log("AAAAAAAAA");
            laughterScript.valueChanging = true;
            if(currentValue != lastValue)
            {
                //Debug.Log("currentval not equal to lastval");
                lastValue = currentValue;
                Debug.Log(currentValue);
                //ChangeTheSound(laughterScript.level-1);
                // stateAudioDict{stateNum : [min int, max int]}
                ChangeTheSound(Random.Range(stateAudioDict[currentValue][0], stateAudioDict[currentValue][1]));

            }
            //laughterScript.valueChanging = true;
        }
        //if (scriptAInstance != null)
        //{

        //    Debug.Log(scriptAInstance.noise);
        //}
    }

    public void ChangeTheSound(int clipIndex) // the index of the sound, 0 for first sound, 1 for the 2nd..etc
    {
        // use one desired logic
        // this will make only one sound to play without interruption
        audioSource.clip = clips[clipIndex];
        audioSource.loop = false;
        audioSource.Play();

        // this will make multiple sound to play with interruption
        audioSource.PlayOneShot(clips[clipIndex]);
        laughterScript.valueChanging = false;
    }
}