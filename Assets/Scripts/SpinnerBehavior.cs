using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerBehavior : MonoBehaviour, IControlHandler {
    public ConfigurableJoint joint;
    public bool spin = false;

    public void onInputActive(bool isActive) {
        spin = isActive;
    }

    void Update() {
        float targetVel = this.spin ? 60.0f : 0.0f;

        joint.targetAngularVelocity = new Vector3(targetVel, 0.0f, 0.0f);
    }
}
