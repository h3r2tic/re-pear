using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandSound : MonoBehaviour
{
    public bool isGrounded = false;
    public bool playOnce = true;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground") 
        {
            isGrounded = true;

            if (playOnce) 
            {
                Debug.Log("Land");
                GetComponent<SimpleSoundModule>().PlayModule();
                playOnce = false;
            }
            
        }
    }
}
