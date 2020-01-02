using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

public class Gun : Interactable {

    public Transform magazineOrigin;
    Magazine magazine;
    public Transform bulletCasingOrigin;
    public GameObject emptyCasingPrefab;
    public float shotsPerSecond;
    public int bulletInChamber;
    bool hasShot;
    float coolDown;
    public bool showRay;
    public float hitForce;
    [Header("Animations")]
    public string shotName;
    public string ammoToChamber;
    public string triggerPress;
    Animator animator;

    [Header("SFX")]
    public AudioClip shot;
    public AudioClip empty;

    private void Start() {
        StartSetUp();
    }

    private void Update() {
        if (hasShot) {
            coolDown += Time.deltaTime;
            if (coolDown > 1 / shotsPerSecond) {
                hasShot = false;
                coolDown = 0;
            }
        }
    }

    public override void Use(bool down) {
        if (down) {
            if (!hasBeenDown == shot && hasShot == false) {
                if(bulletInChamber == 1) {
                    if (shot) {
                        AudioManager.PlaySound(shot);
                    }
                    RaycastHit hit;
                    if (Physics.Raycast(origin.position, origin.forward, out hit, range)) {
                        if (hit.transform.GetComponent<Rigidbody>()) {
                            hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(origin.transform.forward * hitForce, hit.point);
                        }
                    }
                    if(bulletCasingOrigin && emptyCasingPrefab) {
                        Instantiate(emptyCasingPrefab, bulletCasingOrigin.position, bulletCasingOrigin.rotation);
                    }
                    ChamberLoader();
                    AnimatorCheckAndExecute(true);
                } else {
                    if (empty) {
                        AudioManager.PlaySound(empty);
                    }
                }
                hasShot = true;
            }
            hasBeenDown = true;
        } else {
            hasBeenDown = false;
        }
    }
    
    public void AnimatorCheckAndExecute(bool shoot) {
        if (animator) {
            if (bulletInChamber == 0) {
                animator.SetBool(ammoToChamber, false);
            } else {
                animator.SetBool(ammoToChamber, true);
            }
            if (shoot) {
                animator.SetTrigger(shotName);
                animator.SetTrigger(triggerPress);
            }
        }
    }

    public void ChamberLoader() {
        bulletInChamber = 0;
        if (magazine && magazine.bullets > 0) {
            bulletInChamber = 1;
            //using -- caused issues
            magazine.bullets -= 1;
        }
    }

    public override void StartSetUp() {
        base.StartSetUp();
        animator = GetComponent<Animator>();
        if(GetComponentInChildren<Magazine>()) {
            InsertMagazine(GetComponentInChildren<Magazine>().transform);
        }
        ChamberLoader();
        AnimatorCheckAndExecute(false);
    }

    public void InsertMagazine(Transform mag) {
        mag.SetParent(magazineOrigin);
        mag.GetComponent<Rigidbody>().isKinematic = true;
        mag.GetComponent<BoxCollider>().enabled = false;
        magazine = mag.GetComponent<Magazine>();
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
