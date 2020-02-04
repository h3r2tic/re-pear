using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocTrig : MonoBehaviour
{
    public SoundModule VocSound;
    static SoundModule sm;

    private void Awake()
    {
        sm = VocSound;
    }

    public static void PlayVoc()
    {
        sm.PlayModule();
    }
}
