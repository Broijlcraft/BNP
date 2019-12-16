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

        //if (Input.GetMouseButtonDown(0) || IsVrPresentAndAxisUsed(Input.GetAxis(touchInput)) == true) {
        //    Buttt();
        //}

        if (Input.GetMouseButtonDown(0)) {
            Buttt();
        }

        if (XRDevice.isPresent) {
            if (Input.GetButton(touchInput) == true){
                Buttt();
            }
        }

        //print(Input.GetButton(touchInput) + " " + nodeName.ToString());
        //print(Input.GetAxis(triggerInput) + " " + nodeName.ToString());

        if (isButtonDown == true) {
            RaycastHit hit;
            activeDot.gameObject.SetActive(true);
            if (Physics.Raycast(origin.position, origin.forward, out hit, range)) {
                tp = hit.transform;
                p = hit.point;
                SetLinePos(true, origin.position, origin.forward * range + origin.position);
                if (Vector3.Distance(origin.position, hit.point) < range) {
                    activeDot.transform.position = hit.point + ((hit.point - origin.position) * -devider);
                    SetLinePos(true, origin.position, hit.point);
                } else {
                    SetLinePos(true, origin.position, origin.forward * range + origin.position);
                    activeDot.transform.position = origin.forward * range + origin.position;
                }
            } else {
                SetLinePos(true, origin.position, origin.forward * range + origin.position);
                activeDot.transform.position = origin.forward * range + origin.position;
                tp = null;
                p = Vector3.zero;
            }
        } else {
            activeDot.gameObject.SetActive(false);
            SetLinePos(false, Vector3.zero, Vector3.zero);
        }

        if (Input.GetMouseButtonUp(0)) {
            Tele();
        }

        if (XRDevice.isPresent) {
            if (Input.GetButton(touchInput) == false) {
                Tele();
            }
        }

        //if (Input.GetMouseButtonUp(0) || IsVrPresentAndAxisUsed(Input.GetAxis(touchInput)) == false) {
        //    Tele();
        //}
    }

    void Buttt() {
        isButtonDown = true;
    }

    void Tele() {
        if (tp != null && tp.transform.tag == "Teleport") {
            activePlayer.transform.position = p;
        }
        isButtonDown = false;
    }

    void SetLinePos(bool b, Vector3 pos1, Vector3 pos2) {
        if (lineRenderer.enabled != b) {
            lineRenderer.enabled = b;
        }
        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);
    }

    bool IsVrPresentAndAxisUsed(float inputValue) {
        if (inputValue == 1) {
            return true;
        } else {
            return false;
        }
        //if (XRDevice.isPresent && inputAxis == 1) {
        //    return true;
        //} else {
        //    return false;
        //}
    }
}
