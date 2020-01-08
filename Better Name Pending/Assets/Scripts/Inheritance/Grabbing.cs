using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Grabbing : Hands {

    [Space]
    [HideInInspector] public GameObject itemInHand;
    public bool showGizmos;
    public Color gizmosColor;
    public LayerMask layer;

    [HideInInspector] public bool hasGiven;

    bool buttonStillDown;

    private void Start() {
        SetVrInputs();
        CheckPickupEnum();
    }

    private void FixedUpdate() {
        if (XRDevice.isPresent) {
            transform.localPosition = InputTracking.GetLocalPosition(nodeName);
            transform.localRotation = InputTracking.GetLocalRotation(nodeName);
        }
    }
    
    private void Update() {
        if (MouseInputAndVRAxisCheck(1, gripInput, "Useless_Input")) {
            if (buttonStillDown == false) {
                if(hasGiven == false) {
                    switch (pickup) {
                        case VrInputManager.Pickup.hold:                    
                            GrabAndLetGo(origin);
                        break;
                        case VrInputManager.Pickup.toggle:
                            if (itemInHand) {
                                GrabAndLetGo(null);
                            } else {
                                GrabAndLetGo(origin);
                            }
                        break;
                    }
                }
                buttonStillDown = true;
            }
        } else {
            if(buttonStillDown == true) {
                if (pickup == VrInputManager.Pickup.hold) {
                    GrabAndLetGo(null);
                    hasGiven = false;
                }
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
        GameObject coll;
        if (colliders.Length > 0) {
            for (int i = 0; i < colliders.Length; i++) {
                coll = CheckFor(colliders[i]);
                if (coll) {
                    return coll;
                }
            }
        }
        return null;
    }

    GameObject CheckFor(Collider collider) {
        switch (collider.tag) {
            case "Interactable":
                if(collider.GetComponent<Interactable>().onGrab != Interactable.OnGrab.DoNothing) {
                    return collider.gameObject;
                }
            break;
            case "InteractableChild":
                if (collider.GetComponentInParent<Interactable>().onGrab != Interactable.OnGrab.DoNothing) {
                    return collider.GetComponentInParent<Interactable>().gameObject;
                }
            break;
        }
        return null;
    }

    void HeldItemInteract(bool down) {
        if (itemInHand) {
            itemInHand.GetComponent<Interactable>().Use(down);
        } else {

        }
    }

    private void OnDrawGizmos() {
        if (origin) {
            if (showGizmos) {
                Gizmos.color = gizmosColor;
                Gizmos.DrawWireSphere(origin.position, range);
            } else {
                print("No Origin Set");
            }
        }
    }
}
