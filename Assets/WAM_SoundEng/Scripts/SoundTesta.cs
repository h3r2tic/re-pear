﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundTesta : MonoBehaviour
{
    public UnityEvent PlayAction;
    public UnityEvent StopAction;

    private bool doOnce = true;

    private void Start()
    {
        //PlayAction.Invoke();


    }

    /*
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 150, 30), "Play"))
        {
            PlayAction.Invoke();
        }

        
        if (GUI.Button(new Rect(10, 170, 150, 30), "Stop"))
        {
            StopAction.Invoke();
        }
    }*/

    private void Update()
    {
        if (doOnce) 
        {
            PlayAction.Invoke();
            doOnce = false;


        }
    }
}
