using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleControlsForMobile : MonoBehaviour {
    public float touchScale = 2.0f;

    void Start() {
        if (Input.touchSupported) {
            GetComponent<RectTransform>().localScale = new Vector3(touchScale, touchScale, touchScale);
        }
    }
}
