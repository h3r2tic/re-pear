using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMode : MonoBehaviour {

    public float timer = 20.0f; //20sec

    MeasureTravelDistance watch;

    public void StartRace() {
        //remove ability to move objects
        FindObjectsOfType<ClicketyHandler>()[0].enabled = false;
        Cursor.visible = false;
        //
        this.watch = FindObjectsOfType<MeasureTravelDistance>()[0];
        watch.Go();
        //
        this.timer = 20.0f;
    }

    void Update() {
        this.timer -= Time.deltaTime;

        if (this.timer < 0.0f) {
            this.watch.Stop();
            //Display total distance traveled at the end
        }
    }

}
