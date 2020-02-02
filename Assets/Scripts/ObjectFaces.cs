using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFaces : MonoBehaviour {

    public GameObject planePrefab;
    private GameObject planeObj;
    private Vector3 planeRot = new Vector3(90.0f, 180.0f, 180.0f);

    public Material happyMaterial;
    public Material surprisedMaterial;
    public Material anguishedMaterial;

    public float velocityThreshold = 0.2f;
    public Vector3 faceLocalOffset = Vector3.zero;

    bool isDragged = false;
    float switchCoolDown = 0.0f;
    int visibilityMask = 1;

    bool hasSwitchedState = false;
    bool prevStateCahce = false;
    float timeSinceChangedStates = 0.0f;

    Vector3 parentCachePosition;

    // Start is called before the first frame update
    void Start() {
        this.planeObj = Instantiate(this.planePrefab, this.transform.position - new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        this.planeObj.GetComponent<Renderer>().material = this.happyMaterial;

        parentCachePosition = this.transform.position;
        this.visibilityMask = LayerMask.GetMask(new string[] { "Default", "DraggedObject" });
    }

    // Update is called once per frame
    void Update() {
        //always looks at the camera
        this.planeObj.transform.LookAt(Camera.main.transform);
        this.planeObj.transform.Rotate(planeRot.x, planeRot.y, planeRot.z, Space.Self);

        this.planeObj.transform.position = this.transform.position + this.transform.TransformVector(this.faceLocalOffset);

        CheckIfDragged();
        CheckForStateChange();
        if (this.hasSwitchedState) {
            VocTrig.PlayVoc();
        }

        CheckForTerror();

        if (CheckIfObscured()) {
            this.planeObj.SetActive(false);
        } else {
            this.planeObj.SetActive(true);
        }
    }

    void CheckForTerror() {
        if (TerrorScript.timeSinceAccident < 2.0f) {
            this.planeObj.GetComponent<Renderer>().material = this.anguishedMaterial;

            //PlAY terror
        }
    }

    void CheckForStateChange() {
        if (this.timeSinceChangedStates > Random.Range(2.0f, 3.0f) && Input.GetMouseButton(0)) {
            this.hasSwitchedState = false;
            if (this.isDragged == true && this.prevStateCahce == false) {
                this.hasSwitchedState = true;
                this.timeSinceChangedStates = 0.0f;
            }
            this.prevStateCahce = this.isDragged;
        } else {
            this.hasSwitchedState = false;
            this.prevStateCahce = this.isDragged;
        }
        this.timeSinceChangedStates += Time.deltaTime;
    }

    void OnDestroy() {
        if (this.planeObj) {
            Destroy(this.planeObj);
        }
    }

    void CheckIfDragged() {
        if (this.GetComponent<Rigidbody>().velocity.magnitude > velocityThreshold) {
            this.isDragged = true;
            this.parentCachePosition = this.transform.position;
            this.planeObj.GetComponent<Renderer>().material = this.surprisedMaterial;
            this.switchCoolDown = 0.2f;
        } else if (this.switchCoolDown < 0.0f) {
            this.isDragged = false;
            this.planeObj.GetComponent<Renderer>().material = this.happyMaterial;
        }
        this.switchCoolDown -= Time.deltaTime;
    }

    bool CheckIfObscured() {
        RaycastHit hit;
        //TODO: make it shoot ray from the camera towards the object and check whether it hit the most parent obj in the hierarchy
        var ray = this.transform.position - Camera.main.transform.position;
        ray.Normalize();

        if (Physics.Raycast(Camera.main.transform.position, ray, out hit, 10000.0f, this.visibilityMask)) {
            if (hit.transform.root == this.transform.root) {
                return false;
            } else {
                return true;
            }
        }
        return false;
    }


}
