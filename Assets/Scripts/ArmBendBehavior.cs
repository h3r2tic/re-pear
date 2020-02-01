using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBendBehavior : MonoBehaviour, IControlHandler {
    public ConfigurableJoint joint;
    public bool flex = false;

    public void onInputActive(bool isActive) {
        flex = isActive;
    }

    void Update() {
        float targetRot = this.flex ? 60.0f : 0.0f;

        joint.targetRotation = Quaternion.Euler(targetRot, 0.0f, 0.0f);
    }
}
