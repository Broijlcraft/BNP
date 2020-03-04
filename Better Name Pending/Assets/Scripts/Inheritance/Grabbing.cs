using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Grabbing : Hands {

    [Space]
    public GameObject itemInHand;
    public bool showGizmos;
    public Color gizmosColor;
    public LayerMask layer;
    [Header("Animations")]
    public string grab;
    public string letGoOfGrab;
    public string fingerShoot;
    public string gunHold;
    public Animator animator;
    bool buttonZeroStillDown;
    bool buttonOneStillDown;

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
            if (buttonOneStillDown == false) {
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
                if (itemInHand) {
                    if (itemInHand.GetComponent<Interactable>() && itemInHand.GetComponent<Interactable>().specificGrabAnim != "") {
                        HandAnim(itemInHand.GetComponent<Interactable>().specificGrabAnim);
                    }
                } else {
                    HandAnim(grab);
                }
                buttonOneStillDown = true;
            }
        } else {
            if(buttonOneStillDown == true) {
                if (pickup == VrInputManager.Pickup.hold) {
                    GrabAndLetGo(null);
                }
                if (!itemInHand) {
                    animator.ResetTrigger(grab);
                    HandAnim(letGoOfGrab);
                }
                buttonOneStillDown = false;
            }
        }

        if (MouseInputAndVRAxisCheck(0, triggerInput, "Useless_Input")) {
            if (!buttonZeroStillDown) {
                Interact(true, origin);
                buttonZeroStillDown = true;
                if (itemInHand) {
                    HandAnim(grab);
                }
            }
        } else {
            if (buttonZeroStillDown) {
                buttonZeroStillDown = false;
                Interact(false, null);
                if (!itemInHand) {
                    animator.ResetTrigger(grab);
                    HandAnim(letGoOfGrab);
                }
            }
        }
    }

    void HandAnim(string animationName) {
        if (animator) {
            animator.SetTrigger(animationName);
        }
    }

    public void GrabAndLetGo(Transform makeParent) {
        if (makeParent) {
            itemInHand = CheckClosest(Physics.OverlapSphere(origin.position, range));
            if (itemInHand && !itemInHand.GetComponentInParent<Grabbing>()) {
                itemInHand.GetComponent<Interactable>().AttachToHand(makeParent, true);
            } else {
                itemInHand = null;
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
                if (coll && !coll.GetComponent<Interactable>().blockPickUp) {
                    return coll;
                }
            }
        }
        return null;
    }

    GameObject CheckFor(Collider collider) {
        switch (collider.tag) {
            case "Interactable": case "Keycard":
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

    void Interact(bool down, Transform makeParent) {
        if (itemInHand) {
            itemInHand.GetComponent<Interactable>().Use(down);
        } else {
            if (down) {
                if (!itemInHand) {
                    if (origin) {
                        GameObject g = CheckClosest(Physics.OverlapSphere(origin.position, range));
                        if (g && g.GetComponent<Gun>()) {
                            if (g.GetComponent<Gun>().beingHeld || Manager.dev) {
                                itemInHand = g.GetComponent<Gun>().SpecialInteraction(makeParent);
                            }
                        }
                    } else {
                        print("No Origin Set");
                    }
                } else {
                    itemInHand.GetComponent<Gun>().AttachToHand(null, false);
                }
            }
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
