using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocTroller : MonoBehaviour
{

    public enum VocState
    {
        IDLE,
        INTERESTED,
    }

    public static VocState vocState;
    // Start is called before the first frame update
    void Start()
    {
        vocState = VocState.IDLE;

        foreach(SoundModule sm in GetComponentsInChildren<SoundModule>()) 
        {
            sm.PlayModule();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (vocState) 
        {
            case VocState.IDLE:
                //
                for (int i = 0; i < GetComponentsInChildren<SoundModule>().Length; i++)
                {
                    if (i == 0) GetComponentsInChildren<AudioSource>()[i].volume = 0;
                    else GetComponentsInChildren<AudioSource>()[i].volume = 1;
                }
                break;

            case VocState.INTERESTED:
                //
                for (int i = 0; i < GetComponentsInChildren<SoundModule>().Length; i++)
                {
                    if (i == 0) GetComponentsInChildren<AudioSource>()[i].volume = 1;
                    else GetComponentsInChildren<AudioSource>()[i].volume = 0;
                }
                break;
        }
    }
}
