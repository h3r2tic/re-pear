using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameButtonHandler : MonoBehaviour {
    public void CallRestartGame() {
        FindObjectsOfType<GameFlow>()[0].ReloadCurrentScene();
    }
}
