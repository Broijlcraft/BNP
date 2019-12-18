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

    [Header("HideInInspector")]
    public bool anyButton;

    public bool MouseInputAndVRAxisCheck(int mouseButton, string inputString) {
        if (Input.GetMouseButton(mouseButton) || Input.GetAxis(inputString) == 1) {
            return true;
        } else {
            return false;
        }
    }
}
