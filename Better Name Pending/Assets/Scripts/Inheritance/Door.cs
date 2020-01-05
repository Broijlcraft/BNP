using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Door : Interactable {
    
    public GameObject rigidHandle;
    public Rigidbody rigidbodyDoor;
    
    [Header("HideInInspector")]
    public bool locked;

    private void OnDrawGizmos() {
        ShowGizmos(origin, range);
    }
}
