using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class PointTest : Hands {

    public Transform origin;
    public float extraLineRange;

    public GameObject dot;
    public Transform test;
    public float devider;
    public LineRenderer lineRenderer;

    public bool isButtonDown;
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
        if (Input.GetMouseButtonDown(0) || IsVrPresentAndAxisUsed(nodeName, Input.GetAxis(triggerInput)) == true) {
            Buttt();
        }

        if (isButtonDown == true) {
            RaycastHit hit;
            activeDot.gameObject.SetActive(true);
            if (Physics.Raycast(origin.position, origin.forward, out hit, range)) {
                tp = hit.transform;
                p = hit.point;
                SetLinePos(origin.position, origin.forward * range + origin.position);
                if (Vector3.Distance(origin.position, hit.point) < range) {
                    activeDot.transform.position = hit.point + ((hit.point - origin.position) * -devider);
                    SetLinePos(origin.position, hit.point);
                } else {
                    SetLinePos(origin.position, origin.forward * range + origin.position);
                    activeDot.transform.position = origin.forward * range + origin.position;
                }
            } else {
                SetLinePos(origin.position, origin.forward * range + origin.position);
                activeDot.transform.position = origin.forward * range + origin.position;
                tp = null;
                p = Vector3.zero;
            }
        } else {
            activeDot.gameObject.SetActive(false);
            SetLinePos(Vector3.zero, Vector3.zero);
        }

        if (Input.GetMouseButtonUp(0) || IsVrPresentAndAxisUsed(nodeName, Input.GetAxis(triggerInput)) == true) {
            Tele();
        }
    }

    void Buttt() {
        isButtonDown = true;
    }

    void Tele() {
        if ( tp != null && tp.transform.tag == "Teleport") {
            activePlayer.transform.position = p;
        }
        isButtonDown = false;
    }

    void SetLinePos(Vector3 pos1, Vector3 pos2) {
        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);
    }

    bool IsVrPresentAndAxisUsed(XRNode x, float inputAxis) {
        if (XRDevice.isPresent && inputAxis > 0) {
            return true;
        } else {
            return false;
        }
    }
}
