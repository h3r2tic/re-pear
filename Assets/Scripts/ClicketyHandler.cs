using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicketyHandler : MonoBehaviour {
    private Rigidbody draggedBody;
    private Vector3 pointOnDraggedBody;
    private Vector3 normalOnDraggedBody;


    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.rigidbody) {
                    this.onDragStart(hit.rigidbody, hit.point, hit.normal);
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.rigidbody) {
                    this.onDragEnd(hit.rigidbody, hit.point, hit.normal);
                }
            }

            draggedBody = null;
        }
    }

    void onDragStart(Rigidbody body, Vector3 worldPos, Vector3 worldNormal) {
        draggedBody = body;
        pointOnDraggedBody = body.transform.InverseTransformPoint(worldPos);
        normalOnDraggedBody = body.transform.InverseTransformVector(worldNormal);
    }

    void onDragEnd(Rigidbody body, Vector3 worldPos, Vector3 worldNormal) {
        if (!draggedBody || body == draggedBody || draggedBody.gameObject == null) {
            return;
        }

        Quaternion backupRotation = draggedBody.transform.localRotation;

        Vector3 n1 = normalOnDraggedBody;
        Vector3 n2 = draggedBody.transform.InverseTransformVector(worldNormal);
        draggedBody.transform.localRotation *= Quaternion.FromToRotation(n1, -n2);

        var joint = draggedBody.gameObject.AddComponent<ConfigurableJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = pointOnDraggedBody;
        joint.connectedBody = body;
        joint.connectedAnchor = body.transform.InverseTransformPoint(worldPos);
        joint.enableCollision = true;

        var drive = new JointDrive();
        drive.maximumForce = 10.0f;
        drive.positionDamper = 1.0f;
        drive.positionSpring = 10.0f;
        joint.xDrive = drive;
        joint.yDrive = drive;
        joint.zDrive = drive;
        joint.slerpDrive = drive;
        joint.rotationDriveMode = RotationDriveMode.Slerp;

        draggedBody.transform.localRotation = backupRotation;

        StartCoroutine(strengthenJoint(joint));
    }

    private IEnumerator strengthenJoint(ConfigurableJoint joint) {
        for (int i = 1; i <= 10; ++i) {
            yield return new WaitForSeconds(0.1f);
            float mult = (float)i;

            var drive = new JointDrive();
            drive.maximumForce = 100.0f * mult;
            drive.positionDamper = 10.0f;
            drive.positionSpring = 100.0f * mult;
            joint.xDrive = drive;
            joint.yDrive = drive;
            joint.zDrive = drive;
            joint.slerpDrive = drive;
        }
    }
}