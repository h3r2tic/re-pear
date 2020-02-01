using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicketyHandler : MonoBehaviour {
    public Transform cursorObject;
    private LineRenderer cursorLineRenderer;
    private ConfigurableJoint cursorJoint;

    private Rigidbody draggedBody;
    private Vector3 pointOnDraggedBody;
    private Vector3 normalOnDraggedBody;

    void Start() {
        cursorLineRenderer = cursorObject.GetComponent<LineRenderer>();
    }

    void Update() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool gotHit = Physics.Raycast(ray, out hit, float.MaxValue, 1);

        if (gotHit) {
            cursorObject.position = hit.point - ray.direction * 0.25f;
        }

        if (Input.GetMouseButtonDown(0)) {
            if (gotHit) {
                if (hit.rigidbody) {
                    this.onDragStart(hit.rigidbody, hit.point, hit.normal);
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            if (cursorJoint) {
                destroyCursorJoint();
            }

            if (gotHit) {
                if (hit.rigidbody) {
                    this.onDragEnd(hit.rigidbody, hit.point, hit.normal);
                }
            }

            draggedBody = null;
        }

        if (draggedBody) {
            onDragContinue();
        }
    }

    void onDragContinue() {
        var v1 = draggedBody.transform.TransformPoint(pointOnDraggedBody);
        var v2 = cursorObject.position;
        cursorLineRenderer.SetPositions(new Vector3[] { v1, v2 });
    }

    void onDragStart(Rigidbody body, Vector3 worldPos, Vector3 worldNormal) {
        draggedBody = body;
        pointOnDraggedBody = body.transform.InverseTransformPoint(worldPos);
        normalOnDraggedBody = body.transform.InverseTransformVector(worldNormal);

        createCursorJoint();
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
        drive.maximumForce = 100.0f;
        drive.positionDamper = 1.0f;
        drive.positionSpring = 100.0f;
        joint.xDrive = drive;
        joint.yDrive = drive;
        joint.zDrive = drive;
        joint.slerpDrive = drive;
        joint.rotationDriveMode = RotationDriveMode.Slerp;

        draggedBody.transform.localRotation = backupRotation;

        StartCoroutine(strengthenJoint(joint));
    }

    void createCursorJoint() {
        var joint = cursorObject.gameObject.AddComponent<ConfigurableJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = draggedBody;
        joint.connectedAnchor = pointOnDraggedBody;
        joint.enableCollision = false;

        var drive = new JointDrive();
        drive.maximumForce = 50.0f;
        drive.positionDamper = 1.0f;
        drive.positionSpring = 10.0f;
        joint.xDrive = drive;
        joint.yDrive = drive;
        joint.zDrive = drive;

        this.cursorJoint = joint;
        cursorLineRenderer.enabled = true;
    }

    void destroyCursorJoint() {
        Destroy(this.cursorJoint);
        this.cursorJoint = null;
        cursorLineRenderer.enabled = false;
    }

    private IEnumerator strengthenJoint(ConfigurableJoint joint) {
        for (int i = 1; i <= 10; ++i) {
            yield return new WaitForSeconds(0.1f);
            float mult = (float)(i * i);

            var drive = new JointDrive();
            drive.maximumForce = 10.0f * mult;
            drive.positionDamper = 10.0f;
            drive.positionSpring = 10.0f * mult;
            joint.xDrive = drive;
            joint.yDrive = drive;
            joint.zDrive = drive;
            joint.slerpDrive = drive;
        }

        // Wait a bit more, and completely fix the motion
        yield return new WaitForSeconds(0.5f);
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        joint.angularXMotion = ConfigurableJointMotion.Locked;
        joint.angularYMotion = ConfigurableJointMotion.Locked;
        joint.angularZMotion = ConfigurableJointMotion.Locked;

        {
            var drive = new JointDrive();
            joint.xDrive = drive;
            joint.yDrive = drive;
            joint.zDrive = drive;
            joint.slerpDrive = drive;
        }
    }
}