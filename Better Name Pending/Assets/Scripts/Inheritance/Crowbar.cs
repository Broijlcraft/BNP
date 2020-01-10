using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : Interactable { //this is the way, i have spoken
    public float minimumVelocity;
    public Rigidbody rb;
    public Transform[] crowbarParts;
    public void Update() {
        if(velocity.sqrMagnitude >= minimumVelocity) {
            Collider[] colliders = Physics.OverlapSphere(origin.position, range);
            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].CompareTag("Box")) {
                    colliders[i].GetComponent<Box>().BoxBreak();
                    CrowbarBreak();
                    break;
                }
            }
        }
    }
    private void CrowbarBreak() {
        for (int i = 0; i < crowbarParts.Length; i++) {
            Rigidbody rigid2 = crowbarParts[i].GetComponent<Rigidbody>();
            if (rigid2) {
                rigid2.isKinematic = false;
                rigid2.useGravity = true;
            }
            crowbarParts[i].SetParent(null);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos() {
        ShowGizmos(origin, range);
    }
}
