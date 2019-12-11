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
        print(Input.mousePosition) ;
    }

    private void Update() {
        transform.localPosition = InputTracking.GetLocalPosition(nodeName);
        transform.localRotation = InputTracking.GetLocalRotation(nodeName);
        //print(Input.inputString);
        
        //print((Input.GetAxis(triggerInput).ToString()) + " " + nodeName);
    }
}
