using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSoundSource : MonoBehaviour {
    //private PhysicsSoundSystem soundSys;

    public SoundModule Impact;


    private void OnCollisionEnter(Collision collision) {

        //Add obj ref to rest of objects and take away null check
        if(Impact != null) 
        {
            if(collision.relativeVelocity.magnitude > 1) 
            {
                Impact.source.volume = Mathf.Clamp01(collision.relativeVelocity.magnitude * 0.15f);
                Impact.PlayModule();
            }

        }

        //Debug.Log(gameObject + " " + Mathf.Clamp01(collision.relativeVelocity.magnitude));
    }

    void Start() {

    }

    
}


