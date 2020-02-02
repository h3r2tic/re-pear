using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBendBehavior : MonoBehaviour, IControlHandler {
    public ConfigurableJoint joint;
    public bool flex = false;

    private bool playOnce = true;

    public void onInputActive(bool isActive) {
        flex = isActive;

    }

    void Update() {
        bool mode = this.flex ^ WutBehavior.isClose(this.transform);
        float targetRot = mode ? 60.0f : 0.0f;

        joint.targetRotation = Quaternion.Euler(targetRot, 0.0f, 0.0f);


        if (flex) 
        {
            if (playOnce) 
            {
                Debug.Log("ARm sound");
                GetComponent<SimpleSoundModule>().PlayModule();
                playOnce = false;
            }
        }
        else 
        {
            playOnce = true;
        }
        
    }
}
