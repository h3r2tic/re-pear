using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
