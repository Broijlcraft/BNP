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

    [Header("HideInInspector")]
    public bool anyButton;

    private void Start() {
        activeDot = Instantiate(dot, Vector3.zero, Quaternion.identity);
        activeDot.SetActive(false);
        activePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        ButtonDownCheck();

        //print(Input.GetButton(touchInput) + " " + nodeName.ToString());
        //print(Input.GetAxis(triggerInput) + " " + nodeName.ToString());

        if (anyButton == true) {
            RaycastHit hit;
            activeDot.gameObject.SetActive(true);
            if (Physics.Raycast(origin.position, origin.forward, out hit, range)) {
                tp = hit.transform;
                p = hit.point;
                lineRenderer.enabled = true;
                SetLinePos(true, origin.position, origin.forward * range + origin.position);
                if (Vector3.Distance(origin.position, hit.point) < range) {
                    activeDot.transform.position = hit.point + ((hit.point - origin.position) * -devider);
                    SetLinePos(true, origin.position, hit.point);
                } else {
                    SetLinePos(true, origin.position, origin.forward * range + origin.position);
                    activeDot.transform.position = origin.forward * range + origin.position;
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

    void ButtonDownCheck() {
        if (Input.GetMouseButton(0) || Input.GetAxis(touchInput) == 1) {
            anyButton = true;
        } else {
            anyButton = false;
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

    string IsVrPresentAndInputUsed(/*float inputValue,*/ bool b) {
        if (XRDevice.isPresent) {
            if (b == true) {
                return "Pressed";
            } else {
                return "Not_Pressed";
            }
        } else {
            return "No_VR_Present";
        }
    }
}
