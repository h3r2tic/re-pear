using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureTravelDistance : MonoBehaviour {

    Transform centerOfMass;
    Vector3 prevPos;

    public void Go() {
        this.centerOfMass = this.transform.GetChild(0).transform;
        this.prevPos = centerOfMass.position;
    }

    public float Stop() {
        return Vector3.Distance(this.prevPos, this.centerOfMass.position);
    }
}
