using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLight : Interactable {

    public Light lamp;
    public bool onOff; //hide
    
    private void Start() {
        StartSetUp();
    }

    public override void Use(bool down) {
        if (down) {
            if (!hasBeenDown) {
                if (onOff) {
                    lamp.gameObject.SetActive(false);
                    onOff = false;
                } else {
                    lamp.gameObject.SetActive(true);
                    onOff = true;
                }
                hasBeenDown = true;
            }
        } else {
            hasBeenDown = false;
        }
    }

    public override void StartSetUp() {
        base.StartSetUp();
    }
}
