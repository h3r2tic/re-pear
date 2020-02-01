﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFaces : MonoBehaviour {

    public GameObject planePrefab;
    private GameObject planeObj;
    private Vector3 planeRot = new Vector3(90.0f, 180.0f, 180.0f);

    public Material happyMaterial;
    public Material surprisedMaterial;

    bool isDragged = false;
    float switchCoolDown = 0.0f;

    Vector3 parentCachePosition;

    // Start is called before the first frame update
    void Start() {
        this.planeObj = Instantiate(this.planePrefab, this.transform.position - new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        this.planeObj.GetComponent<Renderer>().material = this.happyMaterial;

        parentCachePosition = this.transform.position;
    }

    // Update is called once per frame
    void Update() {
        //always looks at the camera
        this.planeObj.transform.LookAt(Camera.main.transform);
        this.planeObj.transform.Rotate(planeRot.x, planeRot.y, planeRot.z, Space.Self);

        this.planeObj.transform.position = this.transform.position - new Vector3(0.0f, 0.0f, 0.0f);

        CheckIfDragged();
        if (CheckIfObscured()) {
            this.planeObj.SetActive(false);
        } else {
            this.planeObj.SetActive(true);
        }
    }

    void CheckIfDragged() {
        if (this.GetComponent<Rigidbody>().velocity.magnitude > 0.2f) {
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
        var ray = Camera.main.transform.position - this.transform.position;
        ray.Normalize();
        if (Physics.Raycast(this.transform.position + ray * 0.1f, ray, out hit, 10000.0f, 1)) {
            //RaycastHit hit2;
            //if (Physics.Raycast(hit.point, ray, out hit2, 10000.0f)) {
            return true;
            //}

        }
        return false;
    }


}
