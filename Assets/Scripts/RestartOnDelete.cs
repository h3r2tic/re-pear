using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartOnDelete : MonoBehaviour {

    public GameObject dontDispearCanvasPrefab;

    void OnDestroy() {
        var flows = FindObjectsOfType<GameFlow>();
        if (flows.Length > 0 && flows[0] != null) {
            flows[0].DelayedRestart();
            Instantiate(this.dontDispearCanvasPrefab);
        }
    }
}
