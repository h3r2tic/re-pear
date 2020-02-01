using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceMode : MonoBehaviour {

    public float timer = 20.0f; //20sec
    public float pretimer = 2.0f;
    public float prepretimer = 0.3f;

    MeasureTravelDistance watch;
    Text countdown;

    bool isActive = false;

    GameObject stopwatch;

    void Start() {
        this.stopwatch = GameObject.Find("Stopwatch");
        Debug.Log(this.stopwatch);

        this.countdown = GameObject.Find("Countdown").GetComponent<Text>();
        this.stopwatch.SetActive(false);
    }

    public void StartRace() {
        this.isActive = true;
        //remove ability to move objects
        FindObjectsOfType<ClicketyHandler>()[0].enabled = false;
        Cursor.visible = false;
        //
        this.watch = FindObjectsOfType<MeasureTravelDistance>()[0];
        watch.Go();
        //
        this.timer = 20.0f;
        this.pretimer = 2.0f;
        this.prepretimer = 0.5f;
        //expand the floor
        var ground = GameObject.Find("Ground");
        ground.transform.localScale = new Vector3(30.0f, 1.0f, 30.0f);
        //
        GameObject.Find("RaceText").GetComponent<Text>().enabled = true;

    }

    void Update() {

        if (this.isActive) {

            if (this.pretimer > 0.0f) {
                this.pretimer -= Time.deltaTime;
            } else if (this.prepretimer > 0.0f) {
                this.prepretimer -= Time.deltaTime;
                GameObject.Find("RaceText").GetComponent<Text>().text = "Go!";
            } else {

                GameObject.Find("RaceText").GetComponent<Text>().enabled = false;
                this.stopwatch.SetActive(true);

                this.timer -= Time.deltaTime;
                this.countdown.text = this.timer.ToString("0.0");

                if (this.timer < 0.0f) {
                    var score = this.watch.Stop();
                    //Display total distance traveled at the end
                    this.isActive = false;
                    //
                    DisplayScore(score);
                }
            }
        }

    }

    void DisplayScore(float score) {
        GameObject.Find("RaceText").GetComponent<Text>().enabled = true;
        GameObject.Find("RaceText").GetComponent<Text>().text = "Distance traveled: " + score.ToString("0.00");
        GameObject.Find("RaceText").GetComponent<Text>().fontSize = 60;
    }

}
