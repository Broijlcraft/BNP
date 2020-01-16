using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    
    public Interactable rigidHandle;
    public Rigidbody rigidbodyDoor;
    public Keypad keypad;
    bool oldLockState;
    [Header("HideInInspector")]
    public bool locked, handleHeld;

    private void Update() {
        if (rigidHandle.handToFollow) {
            rigidbodyDoor.isKinematic = false;
        } else { 
            rigidbodyDoor.isKinematic = true;        
        }
    }

    public void LockAndUnlock(bool lockState) {
        locked = lockState;
    }
}
