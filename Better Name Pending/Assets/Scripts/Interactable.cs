using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public Transform origin;
    public float maxDistanceFromOrigin;

    public Vector3 setPosition;
    public Vector3 setRotation;

    public bool setPositionAndRotation;

    Vector3 oldPosition;
    Vector3 velocity;
    Vector3 oldRotation;
    Vector3 angularVelocity;


    Transform handToFollow;
    Vector3 originPosition;
    Rigidbody rigidBody;
    [HideInInspector]public bool hasBeenDown;
    bool storeVelocity;
    bool usedVelocity;

    public enum OnGrab {
        Pickup,
        Follow,
        DoNothing
    }

    public OnGrab onGrab;

    private void Start() {
        StartSetUp();
    }

    private void FixedUpdate() {
        if (onGrab == OnGrab.Follow && handToFollow) {
            transform.position = handToFollow.position;
            if (Vector3.Distance(origin.position, handToFollow.position) > maxDistanceFromOrigin) {
                DetachFromHand();
            }
        }

        if(onGrab == OnGrab.Pickup) {
            if (storeVelocity) {
                velocity = (oldPosition - transform.position);
                oldPosition = transform.position;
                angularVelocity = (oldRotation - transform.rotation.eulerAngles);
                oldRotation = transform.rotation.eulerAngles;
                usedVelocity = false;
            } else {
                if (!usedVelocity) {
                    rigidBody.velocity = velocity * -Manager.throwMultiplier;
                    rigidBody.angularVelocity = angularVelocity * -Manager.rotationMultiplier;
                    usedVelocity = true;
                }
            }
        }
    }

    public void DetachFromHand() {
        if (handToFollow) {
            handToFollow.GetComponentInParent<Grabbing>().hasGiven = true;
            handToFollow.GetComponentInParent<Grabbing>().itemInHand = null;
            handToFollow = null;
        }
        transform.localPosition = originPosition;
    }

    public void AttachToHand(Transform makeThisParent, bool shouldSetParent) {
        switch (onGrab) {
            case OnGrab.Follow:
                handToFollow = makeThisParent;
                if(shouldSetParent == false) {
                    DetachFromHand();
                }
            break;
            case OnGrab.Pickup:
                transform.SetParent(makeThisParent);
                if (shouldSetParent) {
                    rigidBody.isKinematic = true;
                    rigidBody.useGravity = false;
                    storeVelocity = true;
                    if (setPositionAndRotation) {
                        //transform.localPosition = setPosition;
                        //transform.localRotation = Quaternion.Euler(setRotation);
                        transform.localPosition = Vector3.zero;
                        transform.localRotation = Quaternion.Euler(Vector3.zero);
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
}
