﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearFaces : MonoBehaviour {

    public Mesh defaultFace;
    public Mesh pearsistentFace;
    public Mesh surprisedFace;
    float cooldown = 0.3f;


    // Start is called before the first frame update
    void Start() {
        this.GetComponent<MeshFilter>().mesh = defaultFace;
    }

    // Update is called once per frame
    void Update() {
        bool anyButtonDown = false;
        if (ClicketyHandler.instance) {
            for (int i = 1; i <= 4; ++i) {
                if (ClicketyHandler.instance.buttonsDown[i]) {
                    anyButtonDown = true;
                }
            }
        }

        if (anyButtonDown || Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Alpha4)) {
            this.GetComponent<MeshFilter>().mesh = pearsistentFace;
            //this.cooldown = 0.3f;
        } else if (this.GetComponent<Rigidbody>().velocity.magnitude > 0.5f) {
            this.GetComponent<MeshFilter>().mesh = surprisedFace;
        } else {
            this.GetComponent<MeshFilter>().mesh = defaultFace;
        }

        //this.cooldown -= Time.deltaTime;
    }
}
