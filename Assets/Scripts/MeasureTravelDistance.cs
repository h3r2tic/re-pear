using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureTravelDistance : MonoBehaviour {
    private float distanceTraveled = 0.0f;
    private bool isActive = false;

    Transform centerOfMass;
    Vector3 prevPos;

    public void Go() {
        this.isActive = true;
        this.distanceTraveled = 0.0f;
        this.centerOfMass = this.transform.GetChild(0).transform;
        this.prevPos = centerOfMass.position;
    }

    public float Stop() {
        this.isActive = false;
        return this.distanceTraveled;
    }

    // Update is called once per frame
    void Update() {
        if (this.isActive) {
            this.distanceTraveled += Vector3.Distance(centerOfMass.position, this.prevPos);
            this.prevPos = centerOfMass.position;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            this.isActive = !this.isActive;
        }
    }
}
