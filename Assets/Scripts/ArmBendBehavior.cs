using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBendBehavior : MonoBehaviour {
    public ConfigurableJoint joint;

    void Update() {
        float targetRot = 0.0f;
        if (Input.GetKey(KeyCode.Space)) {
            targetRot = 60.0f;
        }

        joint.targetRotation = Quaternion.Euler(targetRot, 0.0f, 0.0f);
    }
}
