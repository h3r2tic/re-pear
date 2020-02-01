using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicketyHandler : MonoBehaviour {
    public Transform cursorObject;
    public GameObject attachmentPreviewPrefab;
    public Material activeDragWireMaterial;
    public Material inertDragWireMaterial;
    private LineRenderer cursorLineRenderer;
    private ConfigurableJoint cursorJoint;

    private Rigidbody draggedBody;
    private Vector3 pointOnDraggedBody;
    private Vector3 normalOnDraggedBody;

    List<Joint> recentlyCreatedJoints = new List<Joint>();

    public static ClicketyHandler instance;
    void Awake() {
        instance = this;
    }

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

            if (gotHit && draggedBody) {
                if (canAttachObjects(draggedBody.transform, hit.transform)) {
                    this.onDragEnd(hit.rigidbody, hit.point, hit.normal);
                }
            }

            draggedBody = null;
        }

        if (draggedBody) {
            onDragContinue(hit);
        }
    }

    bool canAttachObjects(Transform a, Transform b) {
        if (!a.GetComponent<Rigidbody>()) {
            return false;
        }

        if (!b.GetComponent<Rigidbody>()) {
            return false;
        }

        return a.root != b.root;
    }

    void onDragContinue(RaycastHit hit) {
        var v1 = draggedBody.transform.TransformPoint(pointOnDraggedBody);
        var v2 = cursorObject.position;
        cursorLineRenderer.SetPositions(new Vector3[] { v1, v2 });

        // Show attachment preview
        if (canAttachObjects(draggedBody.transform, hit.transform)) {

            if (attachmentPreviewPrefab) {
                var meshFilter = attachmentPreviewPrefab.GetComponent<MeshFilter>();
                var meshRenderer = attachmentPreviewPrefab.GetComponent<MeshRenderer>();

                Quaternion rotation = Quaternion.LookRotation(hit.normal) * attachmentPreviewPrefab.transform.localRotation;

                Matrix4x4 m = Matrix4x4.identity;
                m.SetTRS(cursorObject.position, rotation, attachmentPreviewPrefab.transform.localScale);

                Graphics.DrawMesh(meshFilter.sharedMesh, m, meshRenderer.sharedMaterial, 0);
            }

            if (this.activeDragWireMaterial) {
                cursorLineRenderer.sharedMaterial = this.activeDragWireMaterial;
            }
        } else {
            if (this.inertDragWireMaterial) {
                cursorLineRenderer.sharedMaterial = this.inertDragWireMaterial;
            }
        }
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

        onObjectsAttached(joint, body.gameObject, draggedBody.gameObject);
    }

    void onObjectsAttached(ConfigurableJoint joint, GameObject a, GameObject b) {
        var ac = a.transform.root.GetComponent<Controllable>();
        var bc = b.transform.root.GetComponent<Controllable>();

        if (ac) {
            if (0 == ac.connectionCount) {
                ObjectSpawner.instance.disconnectedCount -= 1;
            }

            ac.connectionCount += 1;
        }

        if (bc) {
            if (0 == bc.connectionCount) {
                ObjectSpawner.instance.disconnectedCount -= 1;
            }

            bc.connectionCount += 1;
        }

        this.recentlyCreatedJoints.Add(joint);
    }

    public void onUndoLastAction() {
        if (this.recentlyCreatedJoints.Count > 0) {
            int lastIdx = this.recentlyCreatedJoints.Count - 1;
            var last = this.recentlyCreatedJoints[lastIdx];
            this.recentlyCreatedJoints.RemoveAt(lastIdx);

            if (last) {
                var b0 = last.gameObject.GetComponent<Rigidbody>();
                var b1 = last.connectedBody;

                // Disconnect objects somewhat violently
                if (b0 && b1) {
                    var p0 = b0.transform.position;
                    var p1 = b1.transform.position;
                    var forceAxis = (p0 - p1).normalized;

                    float scl = 200.0f;
                    b0.AddForceAtPosition(forceAxis * scl, p0, ForceMode.Acceleration);
                    b1.AddForceAtPosition(-forceAxis * scl, p1, ForceMode.Acceleration);
                }

                if (b0) {
                    var bc = b0.transform.root.GetComponent<Controllable>();
                    if (bc) {
                        bc.connectionCount -= 1;
                        bc.onDisconnected();
                    }
                }

                if (b1) {
                    var bc = b1.transform.root.GetComponent<Controllable>();
                    if (bc) {
                        bc.connectionCount -= 1;
                        bc.onDisconnected();
                    }
                }

                Destroy(last);
            }
        }
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
        joint.slerpDrive = drive;
        joint.rotationDriveMode = RotationDriveMode.Slerp;

        this.cursorJoint = joint;
        cursorLineRenderer.enabled = true;
    }

    void destroyCursorJoint() {
        Destroy(this.cursorJoint);
        this.cursorJoint = null;
        cursorLineRenderer.enabled = false;
    }

    private IEnumerator strengthenJoint(ConfigurableJoint joint) {
        // Increase strength of the joint over time
        for (int i = 1; i <= 10; ++i) {
            yield return new WaitForSeconds(0.1f);
            if (joint) {
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
        }

        // The linked bodies can sometimes explode physics with this
        /*// Wait a bit more, and completely fix the motion
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
        }*/
    }
}