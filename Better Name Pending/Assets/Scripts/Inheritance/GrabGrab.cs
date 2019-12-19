using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GrabGrab : Hands {

    public GameObject itemInHand;
    public Interactable gun;
    public string testInput;
    public LayerMask interactable;

    private void Update() {
        if (XRDevice.isPresent) {
            transform.localPosition = InputTracking.GetLocalPosition(nodeName);
            transform.localRotation = InputTracking.GetLocalRotation(nodeName);
        }

        if (MouseInputAndVRAxisCheck(1, gripInput)) {
            PickUpAndDropItem();
            print("1 down");
        }

        if (MouseInputAndVRAxisCheck(0, triggerInput)) {
            HeldItemInteract();
        }

        //print(Input.GetButton(gripInput));
        //print(Input.GetAxis(gripInput));
    }

    void PickUpAndDropItem() {
        if (!itemInHand) {
            itemInHand = CheckClosest(Physics.OverlapSphere(origin.position, range));
            if (itemInHand) {
                if (XRDevice.isPresent) {
                    itemInHand.transform.SetParent(transform);
                } else {
                    itemInHand.transform.SetParent(Camera.main.transform);
                }
                itemInHand.GetComponent<Collider>().enabled = false;
                itemInHand.GetComponent<Rigidbody>().useGravity = false;
                itemInHand.GetComponent<Rigidbody>().velocity = Vector3.zero;
                itemInHand.GetComponent<Rigidbody>().isKinematic = true;
            }
        } else {
            itemInHand.GetComponent<Collider>().enabled = true;
            itemInHand.GetComponent<Rigidbody>().isKinematic = false;
            itemInHand.GetComponent<Rigidbody>().useGravity = true;
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
