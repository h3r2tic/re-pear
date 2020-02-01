using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PseudoOrthoCamera : MonoBehaviour {
    public float distance = 100.0f;
    public float yaw = 0.0f;
    public float pitch = 0.0f;
    public float clipPlaneBounds = 20.0f;
    public Vector3 worldOffset;

    // Update is called once per frame
    void Update() {
        transform.localRotation = Quaternion.AngleAxis(this.yaw, Vector3.up) * Quaternion.AngleAxis(this.pitch, Vector3.right);
        transform.position = this.transform.localRotation * new Vector3(0.0f, 0.0f, -this.distance) + worldOffset;

        var cam = GetComponent<Camera>();
        cam.nearClipPlane = Mathf.Max(0.1f, this.distance - clipPlaneBounds);
        cam.farClipPlane = this.distance + clipPlaneBounds;
    }
}
