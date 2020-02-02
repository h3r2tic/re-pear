using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controllable))]
public class BalloonFloatBehavior : MonoBehaviour {
    private Controllable controllable;
    public Rigidbody floatyBit;
    public Rigidbody baseBit;
    public float floatForce = 1.0f;

    void Start() {
        controllable = GetComponent<Controllable>();
    }

    void Update() {
        if (!floatyBit || !baseBit || !controllable) {
            return;
        }

        baseBit.mass = controllable.connectionCount > 0 ? 0.25f : 1.0f;

        floatyBit.AddForce(new Vector3(0.0f, floatForce, 0.0f), ForceMode.Force);
    }
}
