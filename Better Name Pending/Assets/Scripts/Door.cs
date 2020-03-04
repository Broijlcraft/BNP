using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Interactable rigidHandle;
    public Rigidbody rigidbodyDoor;
    public Keypad keypad;
    public KeyPadKey scan;

    [Header("HideInInspector")]
    public bool locked, handleHeld;

    private void Update() {
        if (CheckHeld()) {
            if (keypad && !scan) {
                if(keypad.unlocked) {
                    rigidbodyDoor.isKinematic = false;
                }
            }
            if(!keypad && scan) {
                if (scan.unlocked) {
                    rigidbodyDoor.isKinematic = false;
                }
            }
            if (keypad && scan) {
                if(scan.unlocked && keypad.unlocked) {
                    rigidbodyDoor.isKinematic = false;
                }
            }
            if(!keypad && !scan) {
                rigidbodyDoor.isKinematic = false;
            }
        } else {
            rigidbodyDoor.isKinematic = true;
        }
    }

    bool CheckHeld() {
        if (rigidHandle.handToFollow && !locked) {
            return true;
        } else {
            return false;
        }
    }
}
