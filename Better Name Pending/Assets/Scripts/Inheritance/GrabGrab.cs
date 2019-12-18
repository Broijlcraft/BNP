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
            Collider[] colliders = Physics.OverlapSphere(origin.position, range, interactable);
            float distanceCheck = Mathf.Infinity;
            for (int i = 0; i < colliders.Length; i++) {
                if (Vector3.Distance(colliders[i].transform.position, origin.transform.position) < distanceCheck) {
                    distanceCheck = Vector3.Distance(colliders[i].transform.position, origin.transform.position);
                    itemInHand = colliders[i].gameObject;
                }
            }
            PickUpAndDropItem();
        }
        
        if (MouseInputAndVRAxisCheck(0, triggerInput)) {
            HeldItemInteract();
        }

        //print(Input.GetButton(gripInput));
        //print(Input.GetAxis(gripInput));
    }

    void PickUpAndDropItem() {
        if (itemInHand) {
            itemInHand.transform.SetParent(null);
            itemInHand = null;
        } else {
            itemInHand.transform.SetParent(origin);
        }
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
