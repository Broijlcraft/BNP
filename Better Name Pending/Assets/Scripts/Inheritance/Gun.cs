using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

public class Gun : Interactable {

    public float range;
    public Transform magazineHolder;
    public bool showRay;
    public float hitForce;
    [Header("Animations")]
    public string shotName;
    Animator animator;

    [Header("SFX")]
    public AudioClip shot;

    private void Start() {
        StartSetUp();
    }

    public override void StartSetUp() {
        base.StartSetUp();
        animator = GetComponent<Animator>();
    }

    public override void Use(bool down) {
        if (down) {
            if (!hasBeenDown == shot) {
                if (shot) {
                    AudioManager.PlaySound(shot);
                }
                RaycastHit hit;
                if (Physics.Raycast(origin.position, origin.forward, out hit, range)) {
                    if (hit.transform.GetComponent<Rigidbody>()) {
                        hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(origin.transform.forward * hitForce, hit.point);
                    }
                }
                if (animator) {
                    animator.SetTrigger(shotName);
                }
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
