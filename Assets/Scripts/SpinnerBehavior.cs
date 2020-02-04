﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerBehavior : MonoBehaviour, IControlHandler {
    public ConfigurableJoint joint;
    public bool spin = false;

    public SimpleSoundModule SpinLoop;
    private bool playOnce = true;

    WutTracker wutTracker = new WutTracker();

    public void onInputActive(bool isActive) {
        spin = isActive;
    }

    void Update() {
        float spinDir = wutTracker.getFiltered(this.transform) ? -Mathf.Sin(Time.timeSinceLevelLoad * 10.0f) : 1.0f;
        float targetVel = this.spin ? 60.0f * spinDir : 0.0f;

        joint.targetAngularVelocity = new Vector3(targetVel, 0.0f, 0.0f);

        if (spin) {

            if (playOnce) {
                SpinLoop.PlayModule();
                playOnce = false;
            }
        } else {
            SpinLoop.StopModule();
            playOnce = true;
        }
    }
}
