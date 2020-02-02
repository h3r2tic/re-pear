using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WutTracker {
    public float timeSinceStateChange = 0.0f;
    public bool currentState = false;

    public bool getFiltered(Transform xform) {
        bool newState = WutBehavior.isClose(xform);
        if (newState != currentState && timeSinceStateChange > 0.2f) {
            currentState = newState;
            timeSinceStateChange = 0.0f;
        }

        timeSinceStateChange += Time.deltaTime;

        return currentState;
    }
}

public class WutBehavior : MonoBehaviour {
    public static List<WutBehavior> instances = new List<WutBehavior>();

    void Awake() {
        instances.Add(this);
    }

    void OnDestroy() {
        instances.Remove(this);
    }

    public static bool isClose(Transform xform) {
        foreach (WutBehavior w in instances) {
            if ((w.transform.position - xform.position).magnitude < 1.0f) {
                return true;
            }
        }

        return false;
    }
}
