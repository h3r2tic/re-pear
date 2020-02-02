using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public int buttonIdx = 0;

    public void OnPointerDown(PointerEventData eventData) {
        if (ClicketyHandler.instance) {
            ClicketyHandler.instance.buttonsDown[this.buttonIdx] = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (ClicketyHandler.instance) {
            ClicketyHandler.instance.buttonsDown[this.buttonIdx] = false;
        }
    }
}
