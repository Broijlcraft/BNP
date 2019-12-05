using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTest : MonoBehaviour {

    public Transform origin;
    public float maxRange;

    public Transform test;

    public LineRenderer lineRenderer;
    
    public bool isButtonDown;

    public bool testB;

    private void LateUpdate() {
        Debug.DrawRay(transform.position, transform.forward, Color.red * 1000);
        if (Input.GetMouseButtonDown(0)) {
            isButtonDown = true;
        }

        if (Input.GetMouseButtonUp(0)) {
            isButtonDown = false;
        }

        if (isButtonDown == true) {
            RaycastHit hit;
            if (Physics.Raycast(origin.position, origin.forward, out hit, maxRange)) {
                SetLinePos(origin.transform.position, origin.transform.position);
                if (Vector3.Distance(origin.position, hit.point) < maxRange) {
                    SetLinePos(origin.transform.position, hit.point);
                } else {
                    SetLinePos(origin.transform.position, test.position);
                }
            } else {
                SetLinePos(origin.transform.position, test.position);
            }
        } else {
            SetLinePos(Vector3.zero, Vector3.zero);
        }
    }

    void SetLinePos(Vector3 pos1, Vector3 pos2) {
        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);
    }
}
