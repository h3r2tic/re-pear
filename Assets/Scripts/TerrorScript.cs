using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrorScript : MonoBehaviour {

    public static float timeSinceAccident = 1000.0f;

    private void OnTriggerEnter(Collider collision) {
        timeSinceAccident = 0.0f;
    }

    void Update() {
        timeSinceAccident += Time.deltaTime;
    }
}
