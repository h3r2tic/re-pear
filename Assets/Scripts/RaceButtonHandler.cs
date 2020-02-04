﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceButtonHandler : MonoBehaviour {


    public void StartRace() {
       
        FindObjectsOfType<RaceMode>()[0].StartRace();
    }

    public void UndoStep() {
        if (ClicketyHandler.instance) {
            ClicketyHandler.instance.onUndoLastAction();            
        }
    }
}
