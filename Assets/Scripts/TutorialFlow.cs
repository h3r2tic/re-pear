using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialFlow : MonoBehaviour {
    private int tutorialStep = 0;
    public GameObject mainUI;
    public Text textComponent;

    GameObject undoButton;
    GameObject playButton;
    GameObject controlsGuide;
    GameObject cameraGuide;
    GameObject stopwatch;

    private List<string> tutorialInstructions = new List<string>() {
        "Look around!",
        "Click and drag to construct.",
        "Objects are activated by the key of same color!",
        "Oops! Undo that last move.",
        "When you're ready, you can test your creation!",
        "You'll have 30 seconds to move as far as possible!"
    };

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            NextStep();
        }
    }

    void Start() {
        this.undoButton = GameObject.Find("UndoButton");
        this.playButton = GameObject.Find("NextButton");
        this.controlsGuide = GameObject.Find("ControlsGuide");
        this.cameraGuide = GameObject.Find("ControlsGuideCamera");
        this.stopwatch = GameObject.Find("Stopwatch");

        this.undoButton.SetActive(false);
        this.playButton.SetActive(false);
        this.controlsGuide.SetActive(false);
        this.cameraGuide.SetActive(false);
        this.stopwatch.SetActive(false);
    }

    public void NextStep() {
        if (tutorialStep == 0) {
            this.cameraGuide.SetActive(true);
        } else if (tutorialStep == 1) {
            this.cameraGuide.GetComponent<Blink>().StopBlink();
        } else if (tutorialStep == 2) {
            this.controlsGuide.SetActive(true);
        } else if (tutorialStep == 3) {
            this.undoButton.SetActive(true);
            var childBlinks = this.controlsGuide.GetComponentsInChildren<Blink>();
            foreach (Blink blink in childBlinks) {
                blink.StopBlink();
            }
        } else if (tutorialStep == 4) {
            this.playButton.SetActive(true);
            this.undoButton.GetComponent<Blink>().StopBlink();
        } else if (tutorialStep == 5) {
            this.stopwatch.SetActive(true);
            this.playButton.GetComponent<Blink>().StopBlink();
        }
        if (tutorialStep <= tutorialInstructions.Count - 1) {
            textComponent.text = tutorialInstructions[tutorialStep];
            tutorialStep++;
        } else {
            SceneManager.LoadScene("MainScene");
        }
    }

    public void RestartTutorial() {
        Cursor.visible = true;
        tutorialStep = 0;
        SceneManager.LoadScene("TutorialScene");
    }

}
