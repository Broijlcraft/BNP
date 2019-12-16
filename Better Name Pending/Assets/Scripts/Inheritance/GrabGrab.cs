using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GrabGrab : Hands {

    public GameObject itemInHand;

    private void Start() {
        if (XRDevice.isPresent) {
            print("VR");
        } else {
            print("Keyboard");
        }
    }

    private void Update() {
        transform.localPosition = InputTracking.GetLocalPosition(nodeName);
        transform.localRotation = InputTracking.GetLocalRotation(nodeName);
        Collider[] col = Physics.OverlapSphere(transform.position, range);

        if (Input.GetMouseButtonDown(0)) {
            HeldItemInteract();
        }

        if (XRDevice.isPresent) {
            if (Input.GetButton(touchInput) == true) {
                HeldItemInteract();
            }
        }
    }

    void HeldItemInteract() {
        if (itemInHand.GetComponent<Interactable>()) {

        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
