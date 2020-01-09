using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform objectToFollow;
    public bool useClamp;
    public Vector2 clampValues;

    private void FixedUpdate() {
        if (!useClamp) {
            transform.localPosition = objectToFollow.localPosition;
        } else {
            Vector3 v = transform.localPosition;
            v.z = Mathf.Clamp(objectToFollow.localPosition.z, clampValues.x, clampValues.y);
            transform.localPosition = v;
        }
    }
}
