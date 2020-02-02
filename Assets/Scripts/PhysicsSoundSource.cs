using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSoundSource : MonoBehaviour {
    private PhysicsSoundSystem soundSys;
    private AudioSource audioSource;
    private AudioLowPassFilter filter;
    private Rigidbody rb;



    private void OnCollisionEnter(Collision collision) {
        AudioClip clip = null;
        float mass = 1.0f;
        if (rb) {
            mass = rb.mass;
        }
        float force = mass * collision.impulse.magnitude;

        if (force > 0.1f) {
            clip = soundSys.getRandomClip();
        }

        if (clip/* && !audioSource.isPlaying*/) {
            float volume = Mathf.Min(force * 2.0f, 1.0f);
            //audioSource.pitch = Random.Range(0.5f, 1.25f);
            audioSource.PlayOneShot(clip, volume);
        }
    }

    // Use this for initialization
    void Start() {
        this.soundSys = GameObject.FindObjectOfType<PhysicsSoundSystem>();
        var audioStuff = new GameObject();
        audioStuff.transform.parent = this.transform;
        this.audioSource = audioStuff.AddComponent<AudioSource>();
        //audioSource.volume = 0.6f;
        this.audioSource.playOnAwake = false;
        filter = audioStuff.AddComponent<AudioLowPassFilter>();
        filter.cutoffFrequency = 1000;
        this.rb = gameObject.GetComponent<Rigidbody>();
    }
}
