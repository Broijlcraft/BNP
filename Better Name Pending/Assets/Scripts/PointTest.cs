using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTest : MonoBehaviour {

    public Transform origin;
    public float maxRange;
    public float extraLineRange;

    public GameObject dot;
    public Transform test;

    public LineRenderer lineRenderer;

    public bool isButtonDown;

    GameObject activeDot;

    private void Start() {
        activeDot = Instantiate(dot, Vector3.zero, Quaternion.identity);
        activeDot.SetActive(false);
    }

    private void Update() {
        Debug.DrawRay(transform.position, transform.forward, Color.red * 1000);
        if (Input.GetMouseButtonDown(0)) {
            isButtonDown = true;
        }

        if (Input.GetMouseButtonUp(0)) {
            isButtonDown = false;
        }

        print(Mathf.Sign(origin.forward.x).ToString() + "," + Mathf.Sign(origin.forward.y).ToString() + "," + Mathf.Sign(origin.forward.z).ToString());

        if (isButtonDown == true) {
            RaycastHit hit;
            activeDot.gameObject.SetActive(true);
            if (Physics.Raycast(origin.position, origin.forward, out hit, maxRange)) {
                SetLinePos(origin.position, origin.transform.forward * maxRange + origin.transform.position);
                if (Vector3.Distance(origin.position, hit.point) < maxRange) {
                    activeDot.transform.position = hit.point;
                    SetLinePos(origin.position, hit.point);
                } else {
                    SetLinePos(origin.position, origin.transform.forward * maxRange + origin.transform.position);
                    activeDot.transform.position = origin.transform.forward * maxRange + origin.transform.position;
                }
            } else {
                SetLinePos(origin.position, origin.transform.forward * maxRange + origin.transform.position);
                activeDot.transform.position = origin.transform.forward * maxRange + origin.transform.position;
            }
        } else {
            activeDot.gameObject.SetActive(false);
            SetLinePos(Vector3.zero, Vector3.zero);
        }
    }

    Vector3 Vv(Vector3 v) {


        return v;
    }

    void SetLinePos(Vector3 pos1, Vector3 pos2) {
        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);
    }
}
