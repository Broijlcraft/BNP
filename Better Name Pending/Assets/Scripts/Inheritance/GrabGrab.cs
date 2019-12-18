using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GrabGrab : Hands {

    public GameObject itemInHand;
    public Interactable gun;
    public string testInput;

    private void Update() {
        if (XRDevice.isPresent) {
            transform.localPosition = InputTracking.GetLocalPosition(nodeName);
            transform.localRotation = InputTracking.GetLocalRotation(nodeName);
        }

        Collider[] col = Physics.OverlapSphere(transform.position, range);
        if (Input.GetMouseButtonDown(0)) {
            HeldItemInteract();
        }

        print(Input.GetButton(testInput));
        print(Input.GetAxis(testInput));

        if (XRDevice.isPresent) {
            if (Input.GetButton(triggerInput) == true) {
                HeldItemInteract();
            }
        }
    }

    void HeldItemInteract() {
        if (itemInHand && itemInHand.GetComponent<Interactable>()) {
            itemInHand.GetComponent<Interactable>().Use();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
