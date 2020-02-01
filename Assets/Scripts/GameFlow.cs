using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            FindObjectsOfType<RaceMode>()[0].ClearEndStateUI();
            ReloadCurrentScene();
        }
    }

    public void LoadMainScene() {
        Cursor.visible = true;
        SceneManager.LoadScene("SampleScene");
    }

    public void ReloadCurrentScene() {
        Cursor.visible = true;
        FindObjectsOfType<RaceMode>()[0].ClearEndStateUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
