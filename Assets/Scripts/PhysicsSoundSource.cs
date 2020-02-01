﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSoundSource : MonoBehaviour {
    private PhysicsSoundSystem soundSys;
    private AudioSource audioSource;

    private void OnCollisionEnter(Collision collision) {
        AudioClip clip = null;
        if (collision.impulse.magnitude > 0.25f) {
            clip = soundSys.getRandomClip();
        }

        if (clip) {
            float volume = Mathf.Min(collision.impulse.magnitude / 5.0f, 1.0f);
            audioSource.PlayOneShot(clip, volume);
        }
    }

    // Use this for initialization
    void Start() {
        this.soundSys = GameObject.FindObjectOfType<PhysicsSoundSystem>();
        this.audioSource = this.gameObject.AddComponent<AudioSource>();
    }
}
