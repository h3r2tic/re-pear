using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleSoundModule : MonoBehaviour
{
    public string SoundID;
    public int AudioSourceNo;

    private AudioSource source;

    private int loopCounter = 0;
    private float loopTimer = 0f;
    private float clipLength;

    [Header("Add Audio Clips")]
    public AudioClip clip;

    [Header("Playback Settings")]
    public PlayType playType = PlayType.SINGLE_TRIG;

    [Tooltip("Setting NumLoops to 0 = infinity")]
    public int NumLoops = 0;


    [HideInInspector]
    //public float volume = 0.75f;
    
    public enum PlayType 
    {
        POLY_Trig,
        SINGLE_TRIG,
        LOOP
    }

    void Start()
    {
        if (clip == null)
        {
            Debug.Log("Error: No clip added to module");
            throw new System.Exception("Error: No clip added to module");
        }

        if (GetComponent<AudioSource>() == null) gameObject.AddComponent<AudioSource>();

        source = GetComponents<AudioSource>()[AudioSourceNo];
        source.clip = clip;
        clipLength = clip.length;
    }

    private void PlaySound()
    {

        if (playType == PlayType.POLY_Trig)
        {
            source.PlayOneShot(clip); //took away volume param
        }
        else
        {
            //source.volume = volume;
            source.Play();
        }
    }

    private void LoopTimer()
    {
        //Add fade
        source.Stop();
    }

    public void PlayModule()
    {
        if (playType == PlayType.SINGLE_TRIG || playType == PlayType.POLY_Trig) PlaySound();
        else if (playType == PlayType.LOOP)
        {
            //Loop the sound that gets played
            GetComponent<AudioSource>().loop = true;
            PlaySound();
            if (NumLoops > 0) Invoke("LoopTimer", clipLength * NumLoops);
        }
    }

    public void StopModule()
    {
        //Add fade param
        source.loop = false;
        loopCounter = 0;
        CancelInvoke();
        source.Stop();
    }
}
