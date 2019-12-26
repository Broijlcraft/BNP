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

    bool followHand;
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
        if (followHand && handToFollow) {
            transform.position = handToFollow.position;
            if(Vector3.Distance(origin.position, handToFollow.position) > maxDistanceFromOrigin) {
                StopFollowing();
            }
        }
    }
    
    public void StopFollowing() {
        handToFollow.GetComponent<Grabbing>().GrabAndLetGo(null);
        followHand = false;
        handToFollow = null;
        transform.localPosition = v;
    }

    public void AttachToHand(Transform hand) {
        switch (onGrab) {
            case OnGrab.Follow:
                if(hand == null) {
                    StopFollowing();
                }
                handToFollow = hand;
                followHand = true;
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
