using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hands : MonoBehaviour {
    public XRNode nodeName;
    public string triggerInput;
    public string touchInput;
    public string gripInput;
    public float range;
    public Transform origin;

    public bool grabbing;

    [Header("HideInInspector")]
    public bool anyButton;

    public bool MouseInputAndVRAxisCheck(int mouseButtonValue, string axisInputString, string buttonInputString) {
        if (Input.GetMouseButton(mouseButtonValue) || Input.GetAxis(axisInputString) == 1 || Input.GetButton(buttonInputString)) {
            return true;
        } else {
            return false;
        }
    }
}
