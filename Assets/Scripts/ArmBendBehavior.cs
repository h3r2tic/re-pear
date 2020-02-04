using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBendBehavior : MonoBehaviour, IControlHandler {
    public ConfigurableJoint joint;
    public bool flex = false;
    public bool prevBent = false;

    //Sound Logic
    public SimpleSoundModule ArmSound;
    private bool playOnce = true;

    WutTracker wutTracker = new WutTracker();

    public void onInputActive(bool isActive) {
        flex = isActive;

    }

    void Update() {
        bool bend = this.flex ^ wutTracker.getFiltered(this.transform);
        float targetRot = bend ? 60.0f : 0.0f;

        joint.targetRotation = Quaternion.Euler(targetRot, 0.0f, 0.0f);

        bool needsSound = bend && !prevBent;
        prevBent = bend;

        if (needsSound) {
            if (playOnce) {
                ArmSound.PlayModule();
                playOnce = false;
            }
        } else {
            playOnce = true;
        }

    }
}
