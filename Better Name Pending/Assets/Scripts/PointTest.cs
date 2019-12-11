using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class PointTest : Hands {

    public Transform origin;
    public float maxRange;
    public float extraLineRange;

    public GameObject dot;
    public Transform test;
    public float devider;
    public LineRenderer lineRenderer;

    public bool isButtonDown;
    GameObject activeDot;

    private void Start() {
        activeDot = Instantiate(dot, Vector3.zero, Quaternion.identity);
        activeDot.SetActive(false);
    }

    private void Update() {
        //print(Input.GetAxis(triggerInput).ToString() + " " + nodeName);
        Debug.DrawRay(transform.position, transform.forward, Color.red * 1000);
        if (Input.GetMouseButtonDown(0) || Input.GetAxis(triggerInput) > 0) {
            isButtonDown = true;
        }

        if (Input.GetMouseButtonUp(0) || Input.GetAxis(triggerInput) == 0) {
            isButtonDown = false;
        }

        if (isButtonDown == true) {
            RaycastHit hit;
            activeDot.gameObject.SetActive(true);
            if (Physics.Raycast(origin.position, origin.forward, out hit, maxRange)) {
                SetLinePos(origin.position, origin.forward * maxRange + origin.position);
                if (Vector3.Distance(origin.position, hit.point) < maxRange) {
                    activeDot.transform.position = hit.point + ((hit.point - origin.position) * -devider);
                    SetLinePos(origin.position, hit.point);
                } else {
                    SetLinePos(origin.position, origin.forward * maxRange + origin.position);
                    activeDot.transform.position = origin.forward * maxRange + origin.position;
                }
            } else {
                SetLinePos(origin.position, origin.forward * maxRange + origin.position);
                activeDot.transform.position = origin.forward * maxRange + origin.position;
            }
        } else {
            activeDot.gameObject.SetActive(false);
            SetLinePos(Vector3.zero, Vector3.zero);
        }
    }

    void SetLinePos(Vector3 pos1, Vector3 pos2) {
        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);
    }
}
