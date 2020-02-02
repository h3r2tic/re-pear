using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetTheBodiesHitTheFloor : MonoBehaviour {
    public AudioSource audioSource;
    // Start is called before the first frame update

    public GameObject dontDispearCanvasPrefab;

    private void playDeathScream(GameObject obj) {
        var ds = obj.GetComponent<DeathScream>();
        if (ds && ds.clip) {
            this.audioSource.PlayOneShot(ds.clip);
        }
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.transform) {
            var obj = collision.transform;
            playDeathScream(obj.gameObject);

            while (obj.parent) {
                obj = obj.parent;
                playDeathScream(obj.gameObject);
            }

            if (obj.name == "Pear") {
                Instantiate(this.dontDispearCanvasPrefab);
            }

            Destroy(obj.gameObject);
        }
    }
}
