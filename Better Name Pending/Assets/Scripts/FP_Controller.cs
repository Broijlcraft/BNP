using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Controller : MonoBehaviour {

    public Transform cameraChild;
    public Rigidbody rigid;

    public float walkSpeed = 5;
    public float mouseSensitivity = 100;

    public float topLock = 90;
    public float bottomLock = 270;

    float xAxisClamp;

    private void Update() {
        if (Input.GetButtonDown("Jump")) {

        }
    }

    private void FixedUpdate() {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * walkSpeed * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90.0f) {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(topLock);
        } else if (xAxisClamp < -90.0f) {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.up * mouseX);
        cameraChild.Rotate(Vector3.left * mouseY);
    }
    private void ClampXAxisRotationToValue(float value) {
        Vector3 eulerRotation = cameraChild.localEulerAngles;
        eulerRotation.x = value;
        cameraChild.localEulerAngles = eulerRotation;
    }
}
