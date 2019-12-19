using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Grabbing : Hands {

    public GameObject itemInHand;
    public Interactable gun;
    public string testInput;
    public LayerMask interactable;

    bool buttonStillDown;

    Vector3 controllerVelocity;
    Vector3 oldPosition;

    private void FixedUpdate() {
        if (XRDevice.isPresent) {
            transform.localPosition = InputTracking.GetLocalPosition(nodeName);
            transform.localRotation = InputTracking.GetLocalRotation(nodeName);
            controllerVelocity = oldPosition - transform.position;
            oldPosition = transform.position;
            print(controllerVelocity);
        }
    }

    private void Update() {
        if (MouseInputAndVRAxisCheck(1, gripInput, "Useless_Input")) {
            if(buttonStillDown == false) {
                PickUpAndDropItem();
            }
            buttonStillDown = true;
        } else {
            if (buttonStillDown == true) {
                PickUpAndDropItem();
                buttonStillDown = false;
            }
        }

        if (MouseInputAndVRAxisCheck(0, triggerInput, "Useless_Input")) {
            HeldItemInteract();
        }
    }

    void PickUpAndDropItem() {
        Rigidbody heldRigidbody;
        if (!itemInHand) {
            itemInHand = CheckClosest(Physics.OverlapSphere(origin.position, range));
            if (itemInHand) {
                if (XRDevice.isPresent) {
                    itemInHand.transform.SetParent(transform);
                } else {
                    itemInHand.transform.SetParent(Camera.main.transform);
                }
                if (itemInHand.GetComponent<Rigidbody>()) {
                    heldRigidbody = itemInHand.GetComponent<Rigidbody>();
                    heldRigidbody.useGravity = false;
                    heldRigidbody.velocity = Vector3.zero;
                    heldRigidbody.isKinematic = true;
                }
                if (itemInHand.GetComponent<Interactable>()) {
                    itemInHand.transform.localPosition = itemInHand.GetComponent<Interactable>().setPosition;
                    itemInHand.transform.localRotation = Quaternion.Euler(itemInHand.GetComponent<Interactable>().setRotation);
                }
            }
        } else {
            if (itemInHand.GetComponent<Rigidbody>()) {
                heldRigidbody = itemInHand.GetComponent<Rigidbody>();
                heldRigidbody.isKinematic = false;
                heldRigidbody.useGravity = true;
            }
            itemInHand.transform.SetParent(null);
            itemInHand = null;
        }
    }

    GameObject CheckClosest(Collider[] colliders) {
        if (colliders.Length > 0) {
            for (int i = 0; i < colliders.Length; i++) {
                if(colliders[i].tag == "Interactable") {
                    return colliders[i].gameObject;
                }
            }
        }
        return null;
    }

    void HeldItemInteract() {
        if (itemInHand && itemInHand.GetComponent<Interactable>()) {
            itemInHand.GetComponent<Interactable>().Use();
        }
    }

    private void OnDrawGizmos() {
        if (origin) {
            Gizmos.DrawWireSphere(origin.position, range);
        }
    }
}
