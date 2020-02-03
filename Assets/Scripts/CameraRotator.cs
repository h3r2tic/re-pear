using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PseudoOrthoCamera))]
public class CameraRotator : MonoBehaviour {
    public float rotationSpeed = 5.0f;
    public static bool cameraLeftButtonPressed = false;
    public static bool cameraRightButtonPressed = false;

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
        if (cameraRightButtonPressed || Input.GetKeyDown(KeyCode.X)) {
            cameraRightButtonPressed = false;
            targetAngle += -90.0f;
            GetComponent<SimpleSoundModule>().PlayModule();
        }

        if (cameraLeftButtonPressed || Input.GetKeyDown(KeyCode.Z)) {
            cameraLeftButtonPressed = false;
            targetAngle += 90.0f;
            GetComponent<SimpleSoundModule>().PlayModule();
        }

        angle = Mathf.Lerp(targetAngle, angle, Mathf.Exp(-rotationSpeed * Time.deltaTime));
        poc.yaw = angle;
    }
}
