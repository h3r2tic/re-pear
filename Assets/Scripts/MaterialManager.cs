using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialManager : MonoBehaviour {
    public List<Material> keyMats = new List<Material>();

    public static MaterialManager instance;
    void Awake() {
        instance = this;
    }
}