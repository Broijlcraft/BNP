using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class PointTest : Hands {

    public float extraLineRange;

    public GameObject dot;
    public Transform test;
    public float devider;
    public LineRenderer lineRenderer;
    GameObject activeDot;

    GameObject activePlayer;
    Transform tp;
    Vector3 p;

    private void Start() {
        activeDot = Instantiate(dot, Vector3.zero, Quaternion.identity);
        activeDot.SetActive(false);
        activePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        anyButton = MouseInputAndVRAxisCheck(0, touchInput, "Useless_Input");

        if (anyButton == true && nodeName != XRNode.LeftHand) {
            RaycastHit hit;
            activeDot.gameObject.SetActive(true);
            if (Physics.Raycast(origin.position, origin.forward, out hit, range)) {
                tp = hit.transform;
                p = hit.point;
                lineRenderer.enabled = true;
                SetLinePos(true, origin.position, origin.forward * range + origin.position);
                if (Vector3.Distance(origin.position, hit.point) < range) {
                    if (XRDevice.isPresent) {
                        activeDot.transform.position = hit.point;
                    } else {
                        activeDot.transform.position = hit.point + ((hit.point - origin.position) * -devider);
                    }
                    SetLinePos(true, origin.position, hit.point);
                } else {
                    activeDot.transform.position = origin.forward * range + origin.position;
                    SetLinePos(true, origin.position, origin.forward * range + origin.position);
                }
            } else {
                lineRenderer.enabled = false;
                SetLinePos(true, origin.position, origin.forward * range + origin.position);
                activeDot.transform.position = origin.forward * range + origin.position;
                tp = null;
                p = Vector3.zero;
            }
        } else {
            Teleport();
            activeDot.gameObject.SetActive(false);
            SetLinePos(false, Vector3.zero, Vector3.zero);
        }
    }

    void Teleport() {
        if (tp != null && tp.transform.tag == "Teleport") {
            activePlayer.transform.position = p;
        }
        anyButton = false;
    }

    void SetLinePos(bool b, Vector3 pos1, Vector3 pos2) {
        if (lineRenderer.enabled != b) {
            lineRenderer.enabled = b;
        }
        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);
    }
}
