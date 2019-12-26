using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopVelocityTest : MonoBehaviour {

    public Rigidbody rigidb;

    public bool velocity;
    public bool angularVelocity;

    private void Update() {
        if (Input.GetButtonDown("Jump") && rigidb) {
            if (velocity) {
                rigidb.velocity = Vector3.zero;
            }
            if (angularVelocity) {
                rigidb.angularVelocity = Vector3.zero;
            }
        }
    }
}
