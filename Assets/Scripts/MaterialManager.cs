using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour {
    public List<Material> keyMats = new List<Material>();

    public static MaterialManager instance;
    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}