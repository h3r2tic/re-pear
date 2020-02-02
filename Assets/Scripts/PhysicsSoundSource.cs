using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSoundSource : MonoBehaviour {
    private PhysicsSoundSystem soundSys;
    private AudioSource audioSource;
    private AudioLowPassFilter filter;



    private void OnCollisionEnter(Collision collision) {
        AudioClip clip = null;
        if (collision.impulse.magnitude > 0.3f) {
            clip = soundSys.getRandomClip();
        }

        if (clip && !audioSource.isPlaying) {
            float volume = Mathf.Min(collision.impulse.magnitude / 5.0f, 1.0f);
            audioSource.pitch = Random.Range(0.5f, 1.25f);
            audioSource.PlayOneShot(clip, volume);
        }
    }

    // Use this for initialization
    void Start() {
        this.soundSys = GameObject.FindObjectOfType<PhysicsSoundSystem>();
        this.audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.volume = 0.6f;
        this.audioSource.playOnAwake = false;
        filter = gameObject.AddComponent<AudioLowPassFilter>();
        filter.cutoffFrequency = 1000;
    }
}
