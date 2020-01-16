using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSlide : Interactable {

    public FollowObject followObject;
    public Gun gun;
    public float slideDelay;
    
    bool stillInRange;

    private void Start() {
        gun = GetComponentInParent<Gun>();
    }

    private void LateUpdate() {
        CheckDistance();
    }

    public void CheckDistance() {
        if (followObject) {
            float distance = Vector3.Distance(followObject.transform.localPosition, followObject.localStartPosition + followObject.clampValues);
            print(distance);
            if (distance > followObject.clampValues.y) {
                if (gun && !stillInRange) {
                    gun.ChamberLoader();
                }
                stillInRange = true;
            } else {
                stillInRange = false;
            }
        }
    }

    public void SlideBack() {
        transform.localPosition = setPosition;
        print("Back");
        Invoke("SlideForward", slideDelay);
    }

    void SlideForward() {
        print("Front");
        if (gun.bulletInChamber == 1) {
            transform.localPosition = Vector3.zero;
        }
    }
}
