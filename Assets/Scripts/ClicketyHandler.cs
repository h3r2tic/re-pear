using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicketyHandler : MonoBehaviour {
    private Rigidbody draggedBody;
    private Vector3 pointOnDraggedBody;


    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.rigidbody) {
                    this.onDragStart(hit.rigidbody, hit.point);
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.rigidbody) {
                    this.onDragEnd(hit.rigidbody, hit.point);
                }
            }

            draggedBody = null;
        }
    }

    void onDragStart(Rigidbody body, Vector3 worldPos) {
        Debug.Log("onDragStart");
        draggedBody = body;
        pointOnDraggedBody = body.transform.InverseTransformPoint(worldPos);
    }

    void onDragEnd(Rigidbody body, Vector3 worldPos) {
        Debug.Log("onDragEnd");
        if (!draggedBody || body == draggedBody || draggedBody.gameObject == null) {
            return;
        }

        Debug.Log("Connecting bodies!");
        var joint = draggedBody.gameObject.AddComponent<ConfigurableJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = pointOnDraggedBody;
        joint.connectedBody = body;
        joint.connectedAnchor = body.transform.InverseTransformPoint(worldPos);
        joint.enableCollision = true;

        var posDrive = new JointDrive();
        posDrive.maximumForce = 100.0f;
        posDrive.positionDamper = 10.0f;
        posDrive.positionSpring = 100.0f;
        joint.xDrive = posDrive;
        joint.yDrive = posDrive;
        joint.zDrive = posDrive;
    }
}