using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class PostProcessQualityController : MonoBehaviour {
    public List<PostProcessProfile> profiles = new List<PostProcessProfile>();

    // Start is called before the first frame update
    void Start() {
        int qualityLevel = QualitySettings.GetQualityLevel();
        Debug.Log("current quality level: " + qualityLevel);

        if (qualityLevel < 2) {
            var ssrt = GetComponent<SSRT>();
            if (ssrt) {
                ssrt.enabled = false;
            }
        }

        GetComponent<PostProcessVolume>().profile = this.profiles[qualityLevel];
    }
}
