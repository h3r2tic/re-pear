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
        var p0 = this.prevPos;
        var p1 = this.centerOfMass.position;
        p0.y = 0.0f;
        p1.y = 0.0f;
        return Vector3.Distance(p0, p1);
    }
}
