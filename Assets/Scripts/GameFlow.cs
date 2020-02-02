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
        UISounds.instance.playSelectSound();
        Cursor.visible = true;
        SceneManager.LoadScene("MainScene");
    }

    public void ReloadCurrentScene() {
        UISounds.instance.playSelectSound();
        Cursor.visible = true;
        FindObjectsOfType<RaceMode>()[0].ClearEndStateUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DelayedRestart() {
        this.isDelayedRestart = true;
        this.countdown = 3.0f;
    }
}
