using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceMode : MonoBehaviour {

    public float timer = 20.0f; //20sec

    MeasureTravelDistance watch;
    Text countdown;

    bool isActive = false;

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
        //expand the floor
        var ground = GameObject.Find("Ground");
        ground.transform.localScale = new Vector3(30.0f, 1.0f, 30.0f);
        //
        this.countdown = GameObject.Find("Countdown").GetComponent<Text>();
    }

    void Update() {

        if (this.isActive) {

            this.timer -= Time.deltaTime;
            this.countdown.text = this.timer.ToString("0.0");
            Debug.Log("Workig!");

            if (this.timer < 0.0f) {
                this.watch.Stop();
                //Display total distance traveled at the end
                this.isActive = false;
            }
        }

    }

}
