using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public Transform origin;
    public float range;
    public bool positionToOrigin, showGizmos;
    [Space]
    public AudioManager.AudioGroups audioGroup = AudioManager.AudioGroups.GameSFX;
    public string specificGrabAnim = "Grab";

    [HideInInspector] public Vector3 velocity, angularVelocity;

    Vector3 oldPosition, oldRotation, originPosition;
    public Transform handToFollow;
    Rigidbody rigidBody;
    [HideInInspector]public bool hasBeenDown, beingHeld; //beingHeld necessary for inheritance
    bool usedVelocity, storeVelocity;

    public bool canPickUp;

    public enum OnGrab {
        Pickup,
        Follow,
        DoNothing
    }
    [Space]
    public OnGrab onGrab;

    public enum PositionAndRotation {
        DoNothing,
        ResetPositionAndRotation,
        SetPositionAndRotation
    }
    public PositionAndRotation posAndRot;
    
    public Vector3 setPosition;
    public Vector3 setRotation;

    private void Start() {
        StartSetUp();
    }

    private void Update() {
        if (onGrab == OnGrab.Follow && handToFollow) {
            transform.position = handToFollow.position;
            if (origin) {
                if (Vector3.Distance(origin.position, handToFollow.position) > range) {
                    StopFollowingHand();
                }
            } else {
                print("No Origin Set");
            }
        }
    }

    private void FixedUpdate() {
        if(onGrab == OnGrab.Pickup) {
            if (storeVelocity) {
                beingHeld = true;
                velocity = (oldPosition - transform.position);
                oldPosition = transform.position;
                angularVelocity = (oldRotation - transform.rotation.eulerAngles);
                oldRotation = transform.rotation.eulerAngles;
                usedVelocity = false;
            } else {
                if (!usedVelocity) {
                    beingHeld = false;
                    rigidBody.velocity = velocity * -VrInputManager.throwMultiplier;
                    rigidBody.angularVelocity = angularVelocity * -VrInputManager.rotationMultiplier;
                    usedVelocity = true;
                }
            }
        }
    }

    public void StopFollowingHand() {
        if (handToFollow) {
            Grabbing grabbing = handToFollow.GetComponentInParent<Grabbing>();
            grabbing.itemInHand = null;
            if (grabbing.animator) {
                grabbing.animator.ResetTrigger(grabbing.grab);
                grabbing.animator.SetTrigger(grabbing.letGoOfGrab);
            }
            grabbing = null;
            handToFollow = null;
        }
        if (positionToOrigin) {
            transform.localPosition = origin.localPosition;
        } else {
            transform.localPosition = originPosition;
        }
    }
    
    public void AttachToHand(Transform makeThisParent, bool shouldSetParent) {
        switch (onGrab) {
            case OnGrab.Follow:
                handToFollow = makeThisParent;
                if(shouldSetParent == false) {
                    StopFollowingHand();
                }
            break;
            case OnGrab.Pickup:
                transform.SetParent(makeThisParent);
                if (shouldSetParent) {
                    if (!beingHeld) {
                        rigidBody.isKinematic = true;
                        rigidBody.useGravity = false;
                        storeVelocity = true;
                        switch (posAndRot) {
                            case PositionAndRotation.ResetPositionAndRotation:
                                transform.localPosition = Vector3.zero;
                                transform.localRotation = Quaternion.Euler(Vector3.zero);
                            break;
                            case PositionAndRotation.SetPositionAndRotation:
                                transform.localPosition = setPosition;
                                transform.localRotation = Quaternion.Euler(setRotation);
                            break;
                        }
                    }
                } else {
                    rigidBody.isKinematic = false;
                    rigidBody.useGravity = true;
                    storeVelocity = false;
                }
            break;
        }
    }

    public virtual void StartSetUp() {
        if (origin) {
            originPosition = origin.localPosition;
        }
        rigidBody = GetComponent<Rigidbody>();
    }

    public virtual void Use(bool down) {
        //print("Base use");
    }    

    public virtual GameObject SpecialInteraction(Transform t) {
        return t.gameObject;
    }

    public virtual void ShowGizmos(Transform originTransform, float showRange) {
        if (showGizmos) {
            if (originTransform) {
                Gizmos.DrawWireSphere(originTransform.position, showRange);
            }
        }
    }
}
