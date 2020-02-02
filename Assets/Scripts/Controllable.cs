﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public interface IControlHandler {
    void onInputActive(bool isActive);
}

public class Controllable : MonoBehaviour {
    KeyCode myKey;
    int myButton;
    List<IControlHandler> controlHandlers;

    static int nextColor = 0;
    public int connectionCount = 0;
    public bool isPassive = false;

    private void getDescendants(Transform parent, List<GameObject> list) {
        foreach (Transform child in parent) {
            list.Add(child.gameObject);
            getDescendants(child, list);
        }
    }

    void Awake() {
        if (ObjectSpawner.instance) {
            ObjectSpawner.instance.disconnectedCount += 1;
        }
    }

    // Start is called before the first frame update
    void Start() {
        var desc = new List<GameObject>();
        getDescendants(this.transform, desc);

        this.controlHandlers = new List<IControlHandler>();

        if (!isPassive) {
            foreach (GameObject obj in desc) {
                Component[] comps = obj.GetComponents(typeof(IControlHandler));
                foreach (Component com in comps) {
                    this.controlHandlers.Add(com as IControlHandler);
                }
            }

            var keys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };
            var keyIdx = nextColor++ % 4;
            this.myKey = keys[keyIdx];
            this.myButton = keyIdx + 1;

            foreach (GameObject obj in desc) {
                var mr = obj.GetComponent<MeshRenderer>();
                if (mr && MaterialManager.instance) {
                    mr.sharedMaterial = MaterialManager.instance.keyMats[keyIdx];
                }
            }
        }
    }

    void Update() {
        if (isPassive) {
            return;
        }

        bool buttonDown = false;
        if (ClicketyHandler.instance) {
            buttonDown = ClicketyHandler.instance.buttonsDown[myButton];
        }

        if (Input.GetKey(myKey) || buttonDown) {
            foreach (var h in this.controlHandlers) {
                h.onInputActive(true);
            }
        } else {
            foreach (var h in this.controlHandlers) {
                h.onInputActive(false);
            }
        }
    }

    void OnDestroy() {
        // We need a replacement!
        if (ObjectSpawner.instance) {
            if (0 == connectionCount) {
                ObjectSpawner.instance.disconnectedCount -= 1;
            }
        }
    }

    public void onDisconnected() {
        if (0 == connectionCount && ObjectSpawner.instance) {
            ObjectSpawner.instance.disconnectedCount += 1;
        }
    }
}