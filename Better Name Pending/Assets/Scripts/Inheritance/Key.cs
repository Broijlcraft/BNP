using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractableItems
{
    public void OnCollisionEnter(Collision other) 
    {
        if(other.transform.tag == "Slot")
        {
            gameObject.GetComponentInChildren<FollowObject>().freezeFollow = false;
        }
    }
}
