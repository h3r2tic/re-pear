using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicTrollerTest : MonoBehaviour
{
    AudioSource[] sources;

    public UnityEvent StopAction;

    public float inc = 0.001f;
    float _inc;
    private bool fadeOut = false;
    private bool stop = false;
    public bool stopFade = false;
    const float increset = 0.1f;

    private float[] volBuffer;

    // Start is called before the first frame update
    void Start()
    {
        _inc = inc;
        sources = GetComponentsInChildren<AudioSource>();
        volBuffer = new float[sources.Length];

        for (int i = 0; i < sources.Length; i++)
        {
            volBuffer[i] = sources[i].volume;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (stop)
        {
            if (stopFade) fadeOut = true;
            else 
            {
                StopAction.Invoke();
                stop = false;
            } 
        }


        if (fadeOut) 
        {
            bool fadeDone = true;

            foreach (AudioSource s in sources)
            {
                if (s.volume > 0)
                {
                    if (s.volume > 0.05f) fadeDone = false;
                    s.volume -= .01f;
                }
            }
            inc += inc;

            if (fadeDone) 
            {
                fadeOut = false;
                inc = _inc;
            } 

            if(stop && fadeDone) 
            {
                StopAction.Invoke();
                stop = false;

                for(int i = 0; i < sources.Length; i++) 
                {
                    sources[i].volume = volBuffer[i];
                }

                inc = _inc;
            }
        }



    }

    public void FadeOut() 
    {
        fadeOut = true;
    }

    public void Stop() 
    {
        stop = true;
    }
}
