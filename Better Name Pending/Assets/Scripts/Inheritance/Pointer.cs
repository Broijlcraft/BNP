using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
public class Pointer : Hands {

    [Space]
    public float extraLineRange;

    public string functionName;
    public GameObject dot;
    public Transform test;
    public float devider;
    public bool useBothHands;
    public LineRenderer lineRenderer;
    public bool showGizmos;
    GameObject activeDot;

    GameObject activePlayer;
    Transform tp;
    Vector3 p;

    private void Start() {
        activeDot = Instantiate(dot, Vector3.zero, Quaternion.identity);
        activeDot.SetActive(false);
        activePlayer = GameObject.FindGameObjectWithTag("Player");
        SetVrInputs();
    }

    private void Update() {
        anyButton = MouseInputAndVRAxisCheck(2, touchInput, "Useless_Input");

        if (useBothHands || nodeName != XRNode.LeftHand) {
            if (anyButton == true) {
                RaycastHit hit;
                activeDot.gameObject.SetActive(true);
                if (Physics.Raycast(origin.position, transform.forward, out hit, range)) {
                    tp = hit.transform;
                    p = hit.point;
                    lineRenderer.enabled = true;
                    SetLinePos(true, origin.position, transform.forward * range + origin.position);
                    if (Vector3.Distance(origin.position, hit.point) < range) {
                        if (XRDevice.isPresent) {
                            activeDot.transform.position = hit.point;
                        } else {
                            activeDot.transform.position = hit.point + ((hit.point - origin.position) * -devider);
                        }
                        SetLinePos(true, origin.position, hit.point);
                    } else {
                        activeDot.transform.position = transform.forward * range + origin.position;
                        SetLinePos(true, origin.position, transform.forward * range + origin.position);
                    }
                } else {
                    lineRenderer.enabled = false;
                    SetLinePos(true, origin.position, transform.forward * range + origin.position);
                    activeDot.transform.position = transform.forward * range + origin.position;
                    tp = null;
                    p = Vector3.zero;
                }
            } else {
                PointerInteract();
                activeDot.gameObject.SetActive(false);
                SetLinePos(false, Vector3.zero, Vector3.zero);
            }
        }
    }

    void PointerInteract() {
        if (tp != null){
            if(tp.transform.tag == "Teleport") {
                activePlayer.transform.position = p;
            }
            if (tp.transform.tag == "UI" | tp.transform.tag == "Sfx" | tp.transform.tag == "Music" | tp.transform.tag == "Master"){
                if (MouseInputAndVRAxisCheck(1, touchInput, "Useless_Input") && MouseInputAndVRAxisCheck(1, triggerInput, "Useless_Input")){
                   //tp.GetComponent<Button>()
                }
            }
        }
    }

    void SetLinePos(bool b, Vector3 pos1, Vector3 pos2) {
        if (lineRenderer.enabled != b) {
            lineRenderer.enabled = b;
        }
        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);
    }

    private void OnDrawGizmos() {
        if (showGizmos) {
            if (origin) {
                if (origin) {
                    Debug.DrawRay(origin.position, transform.forward, Color.red * 1000);
                }
            } else {
                print("No Origin Set");
            }
        }
    }
}
