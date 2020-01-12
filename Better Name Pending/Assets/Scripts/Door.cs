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
        if (locked != oldLockState) {

            oldLockState = locked;
        }
    }

    public void LockAndUnlock(bool lockState) {
        locked = lockState;
    }
}
