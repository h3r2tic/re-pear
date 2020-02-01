using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void LoadMainScene() {
        SceneManager.LoadScene("SampleScene");
    }
}
