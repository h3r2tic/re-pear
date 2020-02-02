using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocTrig : MonoBehaviour
{
    static SoundModule sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = GetComponent<SoundModule>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayVoc()
    {
        sm.PlayModule();
    }
}
