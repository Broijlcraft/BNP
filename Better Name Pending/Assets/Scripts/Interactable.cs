using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public Transform origin;
    public float maxDistanceFromOrigin;
    
    public Vector3 setPosition;
    public Vector3 setRotation;

    public bool transferPositionAndRotation;

    Transform handToFollow;
    Vector3 v;

    public enum OnGrab {
        Pickup,
        Follow
    }

    public OnGrab onGrab;

    private void Start() {
        if (origin) {
            v = origin.localPosition;
        }
    }

    private void FixedUpdate() {
        if (handToFollow) {
            transform.position = handToFollow.position;
            if (Vector3.Distance(origin.position, handToFollow.position) > maxDistanceFromOrigin) {
                DetachFromHand();
            }
        }
    }

    //private void Update() {
    //    if (followHand && handToFollow) {
    //        transform.position = handToFollow.position;
    //        if (Vector3.Distance(origin.position, handToFollow.position) > maxDistanceFromOrigin) {
    //            //StopFollowing();
    //            print("Stop");
    //        }
    //    }
    //}

    public void DetachFromHand() {
        handToFollow.GetComponent<Grabbing>().GrabAndLetGo(null);
        handToFollow.GetComponent<Grabbing>().hasGiven = true;
        transform.localPosition = v;
        handToFollow = null;
        print("Stop");
    }

    public void AttachToHand(Transform hand) {
        switch (onGrab) {
            case OnGrab.Follow:
                if(hand == null) {
                    DetachFromHand();
                }
                handToFollow = hand;
            break;
            case OnGrab.Pickup:
            break;
        }
    }

    public virtual void GetControllerPosition(Vector3 position) {

    }

    public virtual void Use() {
        //print("Base use");
    }    
}
