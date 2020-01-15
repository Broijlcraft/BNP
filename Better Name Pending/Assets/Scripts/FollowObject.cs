using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform objectToFollow;
    private Rigidbody rb;
    public bool shouldFollow, useClamp, freezeFollow;
    public Vector2 clampValues;
    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() 
    {
        if (objectToFollow) 
        {
            if (!freezeFollow)
            {
                if (shouldFollow) 
                {
                    if (rb) {
                        rb.MovePosition(objectToFollow.transform.position);
                    } else {
                        transform.position = objectToFollow.position;
                    }
                }
                if(useClamp)
                {
                    Vector3 v = transform.localPosition;
                    v.x = Mathf.Clamp(objectToFollow.localPosition.x, clampValues.x, clampValues.y);
                    transform.localPosition = v;
                }
            }
        }
        else 
        {
            print("No Object To Follow Set");
        }
    }
}
