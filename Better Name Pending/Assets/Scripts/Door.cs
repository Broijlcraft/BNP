using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    
    public Interactable rigidHandle;
    public Rigidbody rigidbodyDoor;

    [Header("HideInInspector")]
    public bool locked, handleHeld;
}
