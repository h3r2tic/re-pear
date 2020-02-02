using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandSound : MonoBehaviour
{
    public bool isGrounded = false;
    public bool playOnce = true;



    private void Update()
    {
        if (playOnce && isGrounded)
        {
            Debug.Log("Land");
            GetComponent<SimpleSoundModule>().PlayModule();
            playOnce = false;
        }
    }
}
