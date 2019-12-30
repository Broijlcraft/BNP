using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

public class Gun : Interactable {

    public AudioClip shoot;
    public Transform magazineHolder;
    public bool showRay;

    private void Start() {
        StartSetUp();
    }

    public override void StartSetUp() {
        base.StartSetUp();
    }

    public override void Use(bool down) {
        if (down) {
            if (!hasBeenDown == shoot) {
                AudioManager.PlaySound(shoot);
                print("SHoot");
            }
            hasBeenDown = true;
        } else {
            hasBeenDown = false;
        }
    }

    private void OnDrawGizmos() {
        if (showRay) {
            if (origin) {
                Debug.DrawRay(origin.position, origin.transform.forward, Color.red * 1000);
            } else {
                print("No Origin Set");
            }
        }
    }
}
