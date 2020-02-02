using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{

    public static UISounds instance;
    void Awake() {
        instance = this;
    }

    public AudioClip undo;
    public AudioClip select;
    public AudioClip startRace;
    public AudioSource output;

    public void playUndoSound() {
        output.PlayOneShot(undo);
    }

    public void playSelectSound() {
        output.PlayOneShot(select);
    }

    public void playStartRaceSound() {
        output.PlayOneShot(startRace);
    }
}
