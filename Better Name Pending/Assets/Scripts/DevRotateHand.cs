using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevRotateHand : MonoBehaviour { 
    public Transform hand;
    public Vector3 rotWhere;
    public float multi;
    
    private void Update() {
        Vector3 rot = hand.localRotation.eulerAngles + rotWhere * (multi * Signing(Input.GetAxis("Mouse ScrollWheel")));
        hand.localRotation = Quaternion.Euler(rot);
    }

    float Signing(float a) {
        if (a < 0) {
            return -1f;
        } else if (a > 0){
            return 1f;
        } else {
            return 0f;
        }
    }
}
