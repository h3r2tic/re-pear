using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSoundSystem : MonoBehaviour {
    public List<AudioClip> audioClips = new List<AudioClip>();

    public AudioClip getRandomClip() {
        if (audioClips.Count > 0) {
            int idx = Random.Range(0, audioClips.Count);
            return audioClips[idx];
        } else {
            return null;
        }
    }
}
