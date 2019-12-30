using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Grabbing : Hands {

    public GameObject itemInHand;
    public bool showGizmos;
    public Color gizmosColor;

    public bool hasGiven;

    bool buttonStillDown;

    private void Start() {
        SetVrInputs();
    }

    private void FixedUpdate() {
        if (XRDevice.isPresent) {
            transform.localPosition = InputTracking.GetLocalPosition(nodeName);
            transform.localRotation = InputTracking.GetLocalRotation(nodeName);
        }
    }

    private void Update() {
        if (MouseInputAndVRAxisCheck(1, gripInput, "Useless_Input")) {
            if (buttonStillDown == false && !hasGiven) {
                GrabAndLetGo(origin);
                buttonStillDown = true;
            }
        } else {
            if (buttonStillDown == true) {
                GrabAndLetGo(null);
                hasGiven = false;
                buttonStillDown = false;
            }
        }

        if (MouseInputAndVRAxisCheck(0, triggerInput, "Useless_Input")) {
            HeldItemInteract(true);
        } else {
            HeldItemInteract(false);
        }
    }

    public void GrabAndLetGo(Transform makeParent) {
        if (makeParent) {
            itemInHand = CheckClosest(Physics.OverlapSphere(origin.position, range));
            if (itemInHand) {
                itemInHand.GetComponent<Interactable>().AttachToHand(makeParent, true);
            }
        } else {
            if (itemInHand) {
                itemInHand.GetComponent<Interactable>().AttachToHand(null, false);
                itemInHand = null;
            }
        }
    }

    GameObject CheckClosest(Collider[] colliders) {
        if (colliders.Length > 0) {
            for (int i = 0; i < colliders.Length; i++) {
                if(colliders[i].CompareTag("Interactable") && colliders[i].GetComponent<Interactable>().onGrab != Interactable.OnGrab.DoNothing) {
                    return colliders[i].gameObject;
                }
            }
        }
        return null;
    }

    void HeldItemInteract(bool down) {
        if (itemInHand) {
            itemInHand.GetComponent<Interactable>().Use(down);
        }
    }

    private void OnDrawGizmos() {
        if (showGizmos && origin) {
            Gizmos.color = gizmosColor;
            Gizmos.DrawWireSphere(origin.position, range);
        }
    }
}
