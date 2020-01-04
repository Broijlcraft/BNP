using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hands : MonoBehaviour {
    public XRNode nodeName;

    public float range;
    public Transform origin;

    public bool grabbing;
    public VrInputManager manager;

    [HideInInspector]
    public string triggerInput, touchInput, gripInput;

    [Header("HideInInspector")]
    public bool anyButton;

    public void SetVrInputs() {
        manager = GameObject.Find("GameManager").GetComponent<VrInputManager>();
        if (nodeName == XRNode.LeftHand) {
            triggerInput = manager.leftHandInput.triggerInput;
            touchInput = manager.leftHandInput.touchInput;
            gripInput = manager.leftHandInput.gripInput;
        } else if(nodeName == XRNode.RightHand) {
            triggerInput = manager.rightHandInput.triggerInput;
            touchInput = manager.rightHandInput.touchInput;
            gripInput = manager.rightHandInput.gripInput;
        }
    }

    public bool MouseInputAndVRAxisCheck(int mouseButtonValue, string axisInputString, string buttonInputString) {
        if (Input.GetMouseButton(mouseButtonValue) || Input.GetAxis(axisInputString) == 1 || Input.GetButton(buttonInputString)) {
            return true;
        } else {
            return false;
        }
    }
}
