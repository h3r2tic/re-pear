using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PseudoOrthoCamera))]
public class CameraRotator : MonoBehaviour {
    public float rotationSpeed = 5.0f;

    PseudoOrthoCamera poc;
    float angle = 0.0f;
    float targetAngle = 0.0f;

    // Start is called before the first frame update
    void Start() {
        poc = GetComponent<PseudoOrthoCamera>();
        targetAngle = angle = poc.yaw;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            targetAngle += -90.0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            targetAngle += 90.0f;
        }

        angle = Mathf.Lerp(targetAngle, angle, Mathf.Exp(-rotationSpeed * Time.deltaTime));
        poc.yaw = angle;
    }
}
