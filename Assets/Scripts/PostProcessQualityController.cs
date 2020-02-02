using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class PostProcessQualityController : MonoBehaviour {
    public List<PostProcessProfile> profiles = new List<PostProcessProfile>();
    int prevQualityLevel = -1;

    void Update() {
        int qualityLevel = QualitySettings.GetQualityLevel();
        if (qualityLevel != prevQualityLevel) {
            prevQualityLevel = qualityLevel;
            Debug.Log("current quality level: " + qualityLevel);

            bool ultra = qualityLevel >= 3;

            var ssrt = GetComponent<SSRT>();
            if (ssrt) {
                ssrt.enabled = qualityLevel >= 2;
                ssrt.resolutionDownscale = ultra ? SSRT.ResolutionDownscale.Full : SSRT.ResolutionDownscale.Half;
                ssrt.stepCount = ultra ? 8 : 4;
            }

            var sun = FindObjectOfType<PCSSLight>();
            if (sun) {
                sun.Blocker_SampleCount = ultra ? 12 : 4;
                sun.UpdateShaderValues();
            }

            GetComponent<PostProcessVolume>().profile = this.profiles[qualityLevel];
        }
    }
}
