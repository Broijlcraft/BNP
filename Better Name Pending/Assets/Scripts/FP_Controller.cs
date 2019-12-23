using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Controller : MonoBehaviour {

    public Transform cameraChild;

    public float walkSpeed = 5;
    public float mouseSensitivity = 100;

    public float topLock = 270;
    public float lock_a = 90;

    float xAxisClamp;

    public Rigidbody rigid;
    public float jumpMultiplier;

    private void Start() {
        SwitchCursor();    
    }

    private void Update() {
        if (Input.GetButtonDown("Special_Input")) {
            SwitchCursor();
        }

        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
    }

    private void FixedUpdate() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontal, 0, vertical) * walkSpeed * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > lock_a) {
            xAxisClamp = lock_a;
            mouseY = 0f;
            ClampXAxisRotationToValue(topLock);
        } else if (xAxisClamp < -lock_a) {
            xAxisClamp = -lock_a ;
            mouseY = 0f;
            ClampXAxisRotationToValue(lock_a);
        }

        transform.Rotate(Vector3.up * mouseX);
        cameraChild.Rotate(Vector3.left * mouseY);
    }
    private void ClampXAxisRotationToValue(float value) {
        Vector3 eulerRotation = cameraChild.localEulerAngles;
        eulerRotation.x = value;
        cameraChild.localEulerAngles = eulerRotation;
    }

    void Jump() {
        rigid.velocity = transform.up * jumpMultiplier;
    }

    void SwitchCursor() {
        if (Cursor.lockState == CursorLockMode.Locked) {
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
