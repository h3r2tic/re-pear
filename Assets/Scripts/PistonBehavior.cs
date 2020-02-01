﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonBehavior : MonoBehaviour, IControlHandler {
    public ConfigurableJoint joint;
    public Transform shaft;
    public bool actuate = false;

    public void onInputActive(bool isActive) {
        actuate = isActive;
    }

    void Update() {
        float targetPos = this.actuate ? -5.0f : 0.0f;
        joint.targetPosition = new Vector3(targetPos, 0.0f, 0.0f);

        var offset = joint.transform.InverseTransformPoint(joint.connectedBody.transform.position);

        // Resize and center the shaft part of the piston
        var scl = shaft.transform.localScale;
        var lp = shaft.localPosition;
        scl.y = offset.x * 0.5f;
        lp.x = offset.x * 0.5f;
        shaft.localPosition = lp;
        shaft.transform.localScale = scl;
    }
}