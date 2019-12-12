using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GrabGrab : Hands {

    private void Start() {
        if (XRDevice.isPresent) {
            print("VR");
        } else {
            print("Keyboard");
        }
    }

    private void Update() {
        transform.localPosition = InputTracking.GetLocalPosition(nodeName);
        transform.localRotation = InputTracking.GetLocalRotation(nodeName);
        Collider[] col = Physics.OverlapSphere(transform.position, range);
        print(col.Length);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
