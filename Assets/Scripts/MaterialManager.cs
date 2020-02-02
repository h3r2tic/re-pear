using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialManager : MonoBehaviour {
    public List<Material> keyMats = new List<Material>();

    public static MaterialManager instance;
    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        var controlsGuide = GameObject.Find("ControlsGuide");

        if (controlsGuide) {
            for (int i = 0; i < keyMats.Count; ++i) {
                var keyGuideImage = controlsGuide.transform.GetChild(i).gameObject.GetComponent<Image>();
                //keyGuideImage.color = keyMats[i].color;
            }
        } else {
            Debug.LogError("Could not find the ControlsGuide object");
        }
    }

    // Update is called once per frame
    void Update() {

    }
}