using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public Transform followThis;
    public Transform makeThisFollow;
    public bool shouldFollow;
    public float maxDistance;

    private void Update() {
        float f = Vector3.Distance(followThis.position, makeThisFollow.position);
        if (f < maxDistance) {
            Vector3 v = makeThisFollow.transform.position;
            v.x = followThis.position.x;
            makeThisFollow.transform.position = v;                
        }
    }
}
