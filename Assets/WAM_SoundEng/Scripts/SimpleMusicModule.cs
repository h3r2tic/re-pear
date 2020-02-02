using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SimpleMusicModule : MonoBehaviour
{
    private AudioSource source;

    [Header("Add Audio Clips")]
    public AudioClip[] clips;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

//Seamless scheduling for music
//https://docs.unity3d.com/ScriptReference/AudioSource.PlayScheduled.html