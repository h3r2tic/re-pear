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
        return Vector3.Distance(this.prevPos, this.centerOfMass.position);
    }
}
