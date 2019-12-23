using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public Vector3 setPosition;
    public Vector3 setRotation;

    public bool transferPositionAndRotation;

    public enum OnGrab {
        Pickup,
        Follow
    }

    public OnGrab onGrab;

    public virtual void GetControllerPosition(Vector3 position) {

    }

    public virtual void Use() {
        //print("Base use");
    }    
}
