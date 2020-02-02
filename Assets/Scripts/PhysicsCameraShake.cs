using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCameraShake : MonoBehaviour {
    Quaternion shakeRotation = Quaternion.identity;
    Vector3 previousPos = Vector3.zero;
    Vector3 previousVel = Vector3.zero;
    Vector3 previousAcc = Vector3.zero;
    Vector3 targetShakeOffset = Vector3.zero;
    Vector3 shakeOffset = Vector3.zero;

    void onImpulse(Vector3 impulse) {
        float m = impulse.magnitude;
        float maxStrength = 0.3f;
        float v = Mathf.Min(1.0f, m * 0.25f) * maxStrength;
        targetShakeOffset += -impulse.normalized * v;
    }

    void OnCollisionEnter(Collision collision) {
        onImpulse(collision.impulse);
    }

    void FixedUpdate() {
        var pos = this.transform.position;
        Vector3 vel = (pos - previousPos) / Time.fixedDeltaTime;
        Vector3 acc = (vel - previousVel) / Time.fixedDeltaTime;
        Vector3 imp = (acc - previousAcc) / Time.fixedDeltaTime;
        previousPos = pos;
        previousVel = vel;
        previousAcc = acc;

        onImpulse(-imp * 0.0002f);
    }

    // Update is called once per frame
    void Update() {
        var cam = Camera.main.GetComponent<PseudoOrthoCamera>();
        targetShakeOffset *= Mathf.Exp(-8.0f * Time.deltaTime);
        shakeOffset = Vector3.Lerp(targetShakeOffset, shakeOffset, Mathf.Exp(-16.0f * Time.deltaTime));

        if (!cam) {
            return;
        }

        //cam.shakeOffset = cam.transform.right * this.shakeOffset.x + cam.transform.up * this.shakeOffset.y;
        cam.shakeOffset = this.shakeOffset;
    }
}
