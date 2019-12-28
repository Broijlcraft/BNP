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
    
    public enum OnGrab {
        Pickup,
        Follow,
        DoNothing
    }

    public OnGrab onGrab;

    private void Start() {
        if (origin) {
            originPosition = origin.localPosition;
        }
    }

    private void FixedUpdate() {
        if (handToFollow && onGrab == OnGrab.Follow) {
            transform.position = handToFollow.position;
            if (Vector3.Distance(origin.position, handToFollow.position) > maxDistanceFromOrigin) {
                DetachFromHand();
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

    public void AttachToHand(Transform makeThisParent, bool setParent) {
        switch (onGrab) {
            case OnGrab.Follow:
                handToFollow = makeThisParent;
                if(setParent == false) {
                    DetachFromHand();
                }
            break;
            case OnGrab.Pickup:
                transform.SetParent(makeThisParent);
                print(setParent);
                if (setParent) {
                    GetComponent<Rigidbody>().isKinematic = true;
                    GetComponent<Rigidbody>().useGravity = false;
                if (setPositionAndRotation) {
                    transform.localPosition = setPosition;
                    transform.localRotation = Quaternion.Euler(setRotation);
                }
                } else {
                    GetComponent<Rigidbody>().isKinematic = false;
                    GetComponent<Rigidbody>().useGravity = true;
                }
            break;
        }
    }

    public virtual void GetControllerPosition(Vector3 position) {

    }

    public virtual void Use() {
        //print("Base use");
    }    
}
