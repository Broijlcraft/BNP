using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;
using UnityEngine.Animations;

public class GrabGrab : MonoBehaviour {
    public XRNode nodeName;
    [Range(0, 100)]
    public float zoom;
    public string inputName;
    public Animator animator;

    public Animation thumbF;
    public Animation indexF;
    public Animation middelF;
    public Animation ringF;
    public Animation pinkyF;

    private void Start() {
        if (XRDevice.isPresent) {
            print("VR");
        } else {
            print("Keyboard");
        }
        //transform.localPosition = UnityEngine.XR.InputTracking.GetLocalPosition(nodeName);
        //transform.localRotation = UnityEngine.XR.InputTracking.GetLocalRotation(nodeName);
        //var inputDevices = new List<InputDevice>();
        //InputDevices.GetDevices(inputDevices);

        //foreach (var device in inputDevices) {
        //    print(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        //}
    }

    private void Update() {
        transform.localPosition = UnityEngine.XR.InputTracking.GetLocalPosition(nodeName);
        transform.localPosition = UnityEngine.XR.InputTracking.GetLocalPosition(nodeName);
        //print((Input.GetAxis(inputName).ToString()) + " " + nodeName);
    }
}
