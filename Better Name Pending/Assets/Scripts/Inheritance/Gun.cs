using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

public class Gun : Interactable {

    public Transform magazineOrigin;
    public Magazine magazine;
    public Transform bulletCasingOrigin;
    public FollowObject followingSlide;
    public GunSlide slideToFollow;
    public float ejectForce;
    public GameObject emptyCasingPrefab, bulletPrefab, defaultBulletHole;
    public float shotsPerSecond;
    [HideInInspector] public int bulletInChamber;
    bool hasShot;
    float coolDown;
    public bool showRay;
    public float hitForce;
    [Header("Animations")]
    public string triggerPress;
    [Header("SFX")]
    public AudioClip shot;
    public AudioClip empty;

    private void Start() {
        StartSetUp();
        if (followingSlide) {
            slideToFollow = followingSlide.objectToFollow.GetComponent<GunSlide>();
            slideToFollow.SlideBack();
        }
        ChamberLoader();
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
                if (bulletInChamber == 1) {
                    if (shot) {
                        AudioManager.PlaySound(shot, audioGroup);
                    }
                    RaycastHit hit;
                    if (Physics.Raycast(origin.position, origin.forward, out hit, range)) {
                        if (hit.transform.GetComponent<DoSomethingWhenShot>()) {
                            hit.transform.GetComponent<DoSomethingWhenShot>().GetShot(hit.point, hit.normal);
                        } else {
                            GameObject hole = Instantiate(defaultBulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                            hole.transform.SetParent(hit.transform);
                        }
                        if (hit.transform.GetComponent<Rigidbody>() && Manager.dev) {
                            hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(origin.transform.forward * hitForce, hit.point);
                        }
                    }
                    slideToFollow.SlideBack();
                } else {
                    if (empty) {
                        AudioManager.PlaySound(empty, audioGroup);
                    }
                }
                hasShot = true;
            }
            hasBeenDown = true;
        } else {
            hasBeenDown = false;
        }
    }

    public void EjectShell(GameObject shell) {
        if (shell && bulletCasingOrigin) {
            GameObject g = Instantiate(shell, bulletCasingOrigin.position, bulletCasingOrigin.rotation);
            g.GetComponent<Rigidbody>().AddForce(bulletCasingOrigin.forward * ejectForce);
        }
    }

    public void ChamberLoader() {
        bulletInChamber = 0;        
        InsertBullet();
    }

    void InsertBullet() {
        if (Manager.dev == true) {
            bulletInChamber = 1;
        } else {
            if (magazine && magazine.bullets > 0) {
                bulletInChamber = 1;
                magazine.bullets -= 1;
            }
        }
    }

    public override GameObject SpecialInteraction(Transform setParent) {
        if (setParent) {
            slideToFollow.transform.position = followingSlide.transform.position;
            followingSlide.enabled = true;
            slideToFollow.AttachToHand(setParent, true);
            return slideToFollow.gameObject;
        } else {
            slideToFollow.AttachToHand(setParent, false);
            return null;
        }
    }

    public override void StartSetUp() {
        base.StartSetUp();
        if (GetComponentInChildren<Magazine>()) {
            InsertMagazine(GetComponentInChildren<Magazine>().transform);
        }
    }

    public void InsertMagazine(Transform mag) {
        mag.SetParent(magazineOrigin);
        mag.GetComponent<Rigidbody>().isKinematic = true;
        mag.GetComponent<BoxCollider>().enabled = false;
        magazine = mag.GetComponent<Magazine>();
        mag.transform.localPosition = Vector3.zero;
        mag.transform.localRotation = Quaternion.Euler(Vector3.zero);
        magazine.magazine.transform.localPosition = Vector3.zero;
        magazine.magazine.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    private void OnDrawGizmos() {
        if (showRay) {
            if (origin) {
                Debug.DrawRay(origin.position, origin.transform.forward, Color.red * 1000);
            } else {
                print("No Origin Set");
            }
            if (bulletCasingOrigin) {
                Debug.DrawRay(bulletCasingOrigin.position, bulletCasingOrigin.forward, Color.blue * 1000);
            } else {
                print("No Bulletcasing Origin Set");
            }
        }
    }
}
