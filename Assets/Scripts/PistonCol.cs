using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonCol : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            GetComponentsInParent<LandSound>()[0].isGrounded = true;

            Debug.Log("pist");

        }
    }
}
