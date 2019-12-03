using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTest : MonoBehaviour {

    public Transform origin;
    public float maxRange;

    public Transform test;

    public LineRenderer lineRenderer;
    
    bool isButtonDown;

    private void Update() {
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
                lineRenderer.SetPosition(0, origin.transform.position);
                if (Vector3.Distance(origin.position, hit.point) < maxRange) {
                    lineRenderer.SetPosition(1, hit.point);
                } else {
                    lineRenderer.SetPosition(1, test.position);
                }
            } else {
                lineRenderer.SetPosition(0, origin.transform.position);
                lineRenderer.SetPosition(1, test.position);
            }
        } else {
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }
    }
}
