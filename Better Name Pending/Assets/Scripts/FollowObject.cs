using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform objectToFollow;
    public bool shouldFollow, useClamp;
    public Vector2 clampValues;

    private void FixedUpdate() {
        if (objectToFollow) {
            if (!useClamp) {
                transform.localPosition = objectToFollow.localPosition;
            } else {
                Vector3 v = transform.localPosition;
                v.x = Mathf.Clamp(objectToFollow.localPosition.x, clampValues.x, clampValues.y);
                transform.localPosition = v;
            }
        } else {
            print("No Object To Follow Set");
        }
    }
}
