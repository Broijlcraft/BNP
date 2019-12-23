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

    public GameObject cam;

    bool buttonStillDown;

    //Vector3 controllerVelocity;
    //Vector3 oldPosition;

    private void FixedUpdate() {
        if (XRDevice.isPresent) {
            transform.localPosition = InputTracking.GetLocalPosition(nodeName);
            transform.localRotation = InputTracking.GetLocalRotation(nodeName);
            //controllerVelocity = (oldPosition - transform.position)*Time.deltaTime;
            //oldPosition = transform.position;
            //print(controllerVelocity);
        }
    }

    private void Update() {
        if (MouseInputAndVRAxisCheck(1, gripInput, "Useless_Input")) {
            if(buttonStillDown == false) {
                GrabAndLetGo();
                buttonStillDown = true;
            }
        } else {
            if (buttonStillDown == true) {
                GrabAndLetGo();
                buttonStillDown = false;
            }
        }

        if (MouseInputAndVRAxisCheck(0, triggerInput, "Useless_Input")) {
            HeldItemInteract();
        }
    }

    void GrabAndLetGo() {
        Rigidbody heldRigidbody;
        if (!itemInHand) {
            itemInHand = CheckClosest(Physics.OverlapSphere(origin.position, range));
            if (itemInHand) {
                if (XRDevice.isPresent) {
                    //if (itemInHand.GetComponent<Interactable>()) {
                    //    switch itemInHand.GetComponent<Interactable>().onGrab{

                    //    }
                    //} else {
                    //    itemInHand.transform.SetParent(transform);
                    //}
                    itemInHand.transform.SetParent(transform);
                } else {
                    itemInHand.transform.SetParent(cam.transform);
                }
                if (itemInHand.GetComponent<Rigidbody>()) {
                    heldRigidbody = itemInHand.GetComponent<Rigidbody>();
                    heldRigidbody.useGravity = false;
                    heldRigidbody.velocity = Vector3.zero;
                    heldRigidbody.isKinematic = true;
                }
                if (itemInHand.GetComponent<Interactable>()) {
                    Interactable interactableInHand = itemInHand.GetComponent<Interactable>();
                    if (interactableInHand.transferPositionAndRotation) {
                        itemInHand.transform.localRotation = Quaternion.Euler(interactableInHand.setRotation);
                        itemInHand.transform.localPosition = interactableInHand.setPosition;
                    }
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
                    print("Coll");
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
