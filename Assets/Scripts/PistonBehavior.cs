using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonBehavior : MonoBehaviour, IControlHandler {
    public ConfigurableJoint joint;
    public Transform shaft;
    public bool actuate = false;
    public bool prevExtended = false;
    WutTracker wutTracker = new WutTracker();

    //Sound Vars
    public SimpleSoundModule PistonJump;
    private bool playOnce = false;


    public void onInputActive(bool isActive) {
        actuate = isActive;
    }

    void Update() {
        bool extend = this.actuate ^ wutTracker.getFiltered(this.transform);
        float targetPos = extend ? -5.0f : 0.0f;
        joint.targetPosition = new Vector3(targetPos, 0.0f, 0.0f);

        var offset = joint.transform.InverseTransformPoint(joint.connectedBody.transform.position);

        // Resize and center the shaft part of the piston
        var scl = shaft.transform.localScale;
        var lp = shaft.localPosition;
        scl.y = offset.x * 0.5f;
        lp.x = offset.x * 0.5f;
        shaft.localPosition = lp;
        shaft.transform.localScale = scl;

        bool needsSound = extend && !prevExtended;
        prevExtended = extend;

        if (needsSound) {
            if (playOnce) {

                PistonJump.PlayModule();
                playOnce = false;
            }
        } else {
            playOnce = true;
        }
    }
}
