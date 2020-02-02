using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controllable))]
[RequireComponent(typeof(LineRenderer))]
public class BalloonFloatBehavior : MonoBehaviour {
    private Controllable controllable;
    private LineRenderer lineRenderer;
    public Rigidbody floatyBit;
    public Rigidbody baseBit;
    public float floatForce = 1.0f;

    void Start() {
        controllable = GetComponent<Controllable>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update() {
        if (!floatyBit || !baseBit || !controllable) {
            return;
        }

        lineRenderer.SetPositions(new Vector3[] { floatyBit.transform.position, baseBit.transform.position });

        baseBit.mass = controllable.connectionCount > 0 ? 0.25f : 1.0f;

        floatyBit.AddForce(new Vector3(0.0f, floatForce, 0.0f), ForceMode.Force);
    }
}
