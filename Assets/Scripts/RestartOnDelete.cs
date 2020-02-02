using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartOnDelete : MonoBehaviour {
    void OnDestroy() {
        FindObjectsOfType<GameFlow>()[0].DelayedRestart();
    }
}
