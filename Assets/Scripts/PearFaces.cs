using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearFaces : MonoBehaviour {

    public Mesh defaultFace;
    public Mesh pearsistentFace;
    float cooldown = 0.3f;


    // Start is called before the first frame update
    void Start() {
        this.GetComponent<MeshFilter>().mesh = defaultFace;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            this.GetComponent<MeshFilter>().mesh = pearsistentFace;
            //this.cooldown = 0.3f;
        } else {
            this.GetComponent<MeshFilter>().mesh = defaultFace;
        }

        //this.cooldown -= Time.deltaTime;
    }
}
