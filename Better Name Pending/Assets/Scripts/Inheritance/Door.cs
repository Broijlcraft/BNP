using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {
    public GameObject handle;

    public override void GetControllerPosition(Vector3 position) {
        handle.transform.position = position;
    }
}
