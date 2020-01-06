using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour {

    public Transform origin;
    public float range;
    public bool showGizmos;
    
    Vector3 oldPosition;
    Vector3 velocity;
    Vector3 oldRotation;
    Vector3 angularVelocity;

    Transform handToFollow;
    Vector3 originPosition;
    Rigidbody rigidBody;
    [HideInInspector]public bool hasBeenDown;
    [HideInInspector]public bool storeVelocity;
    [HideInInspector]public bool beingHeld; //necessary for inheritance
    bool usedVelocity;

    public enum OnGrab {
        Pickup,
        Follow,
        DoNothing
    }
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

    private void FixedUpdate() {
        if (onGrab == OnGrab.Follow && handToFollow) {
            transform.position = handToFollow.position;
            if (Vector3.Distance(origin.position, handToFollow.position) > range) {
                StopFollowingHand();
            }
        }

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
            handToFollow.GetComponentInParent<Grabbing>().hasGiven = true;
            handToFollow.GetComponentInParent<Grabbing>().itemInHand = null;
            handToFollow = null;
        }
        transform.localPosition = originPosition;
    }
    //
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

    public virtual void ShowGizmos(Transform originTransform, float showRange) {
        if (showGizmos) {
            if (originTransform) {
                Gizmos.DrawWireSphere(originTransform.position, showRange);
            }
        }
    }
}
