using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour {

    float countdown = 1.0f;
    bool isDelayedRestart = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            FindObjectsOfType<RaceMode>()[0].ClearEndStateUI();
            ReloadCurrentScene();
        }

        if (this.isDelayedRestart) {
            this.countdown -= Time.deltaTime;
            if (this.countdown < 0.0f) {
                this.isDelayedRestart = false;
                this.ReloadCurrentScene();
            }
        }
    }

    public void LoadMainScene() {
        Cursor.visible = true;
        SceneManager.LoadScene("MainScene");
    }

    public void ReloadCurrentScene() {
        Cursor.visible = true;
        FindObjectsOfType<RaceMode>()[0].ClearEndStateUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DelayedRestart() {
        this.isDelayedRestart = true;
        this.countdown = 1.0f;
    }
}
