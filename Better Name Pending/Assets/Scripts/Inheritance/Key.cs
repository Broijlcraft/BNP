using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractableItems
{
    public Component[] followObject;
    public void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Slot")
        {
            followObject = other.gameObject.GetComponentsInChildren<FollowObject>();

            foreach (FollowObject scripts in followObject)
            {
                scripts.freezeFollow = false;
            }
            other.gameObject.GetComponentInParent<Rigidbody>().useGravity = true;
            other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
