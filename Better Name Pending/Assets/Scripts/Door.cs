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
        rigidbodyDoor.isKinematic = CheckHeld();
    }
    
    bool CheckLocked() {

    }

    bool CheckHeld() {
        if (rigidHandle.handToFollow && !locked) {
            return false;
        } else {
            return true;
        }
    }
}
