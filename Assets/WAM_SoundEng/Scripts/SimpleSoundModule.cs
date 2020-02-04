using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleSoundModule : MonoBehaviour {

    [HideInInspector]
    public AudioSource source;

    //private int loopCounter = 0;
    //private float loopTimer = 0f;
    private float clipLength;
    private bool fadingOut = false;

    [Header("Add Audio Clips")]
    public AudioClip clip;

    [Header("Playback Settings")]
    public PlayType playType = PlayType.SINGLE_TRIG;

    public bool DontInterrupt = true;

    [Tooltip("Setting NumLoops to 0 = infinity")]
    public int NumLoops = 0;


    [HideInInspector]
    //public float volume = 0.75f;

    public enum PlayType {
        POLY_Trig,
        SINGLE_TRIG,
        LOOP
    }

    void Start() {
        if (clip == null) {
            Debug.Log("Error: No clip added to module");
            throw new System.Exception("Error: No clip added to module");
        }

        if (GetComponent<AudioSource>() == null) source = gameObject.AddComponent<AudioSource>();
        else source = GetComponent<AudioSource>();

        source.clip = clip;
        clipLength = clip.length;
    }

    private void PlaySound() {
        this.fadingOut = false;
        //source.volume = 1.0f;
        //Debug.Log(gameObject.transform.root.gameObject.name);
        if (source == null) Debug.Log(gameObject + "derr");
        if (playType == PlayType.POLY_Trig) {
            source.PlayOneShot(clip); //took away volume param
        } else {
            //source.volume = volume;

            if (DontInterrupt && !source.isPlaying) source.Play();
            else if(!DontInterrupt) source.Play();
        }
    }

    private void LoopTimer() {
        // TODO: fade
        source.Stop();
    }

    public void PlayModule() {
        if (playType == PlayType.SINGLE_TRIG || playType == PlayType.POLY_Trig) PlaySound();
        else if (playType == PlayType.LOOP) {
            //Loop the sound that gets played
            GetComponent<AudioSource>().loop = true;
            PlaySound();
            if (NumLoops > 0) Invoke("LoopTimer", clipLength * NumLoops);
        }
    }

    public void StopModule() {
        //Add fade param
        source.loop = false;
        //loopCounter = 0;
        CancelInvoke();
        fadeOut();
    }

    public void fadeOut() {
        this.fadingOut = true;
        //source.Stop();
        StartCoroutine(volumeFader());
    }

    IEnumerator volumeFader() {
        float v = 1.0f;
        while (v > 0.0f) {
            v -= 0.2f;
            if (this.fadingOut) {
                source.volume = Mathf.Max(v, 0.0f);
                yield return new WaitForSecondsRealtime(0.01f);
            } else {
                break;
            }
        }
    }
}
