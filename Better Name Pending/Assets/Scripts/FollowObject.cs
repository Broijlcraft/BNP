using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform objectToFollow;
    private Rigidbody rb;
    public bool shouldFollow, useClamp, freezeFollow, dontUseRigid;
    public Vector3 clampValues;

    public bool x,y,z;
    [HideInInspector] public Vector3 localStartPosition;
    [HideInInspector] public Vector3 globalStartPosition;
    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
        localStartPosition = transform.localPosition;
        globalStartPosition = transform.position;
    }
    private void FixedUpdate() 
    {
        if (objectToFollow) 
        {
            if (!freezeFollow)
            {
                if (shouldFollow) 
                {
                    if (rb && !dontUseRigid) {
                        rb.MovePosition(objectToFollow.transform.position);
                    } else {
                        transform.position = objectToFollow.position;
                    }
                }
                if(useClamp)
                {
                    Vector3 v = transform.localPosition;
                    if(x){
                        v.x = Mathf.Clamp(objectToFollow.localPosition.x, clampValues.x, clampValues.y);          
                    }
                    if(y){   
                        v.y = Mathf.Clamp(objectToFollow.localPosition.y, clampValues.x, clampValues.y);
                    }
                    if(z){
                        v.z = Mathf.Clamp(objectToFollow.localPosition.z, clampValues.x, clampValues.y);
                    }
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
