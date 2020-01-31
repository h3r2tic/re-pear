using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicketyHandler : MonoBehaviour {
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            Debug.Log ("pearfect click!");
        }
    }
}