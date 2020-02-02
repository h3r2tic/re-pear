using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUi : MonoBehaviour {

    bool isEnabled = true;

    // Update is called once per frame
    void Update() {
        if (isEnabled) {
            if (Input.GetKey(KeyCode.Space)) {
                this.GetComponent<Canvas>().enabled = false;
            } else {
                this.GetComponent<Canvas>().enabled = true;
            }
        }

    }

    public void EnableMe() {
        this.isEnabled = false;
    }
}
