using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ManagerUsage : MonoBehaviour {

    public GameObject devUiPanel;
    public GameObject vrPlayer;
    public GameObject pcPlayer;

    private void Awake() {
        if (XRDevice.isPresent) {
            pcPlayer.SetActive(false);
            vrPlayer.SetActive(true);
        } else {
            vrPlayer.SetActive(false);
            pcPlayer.SetActive(true);
        }
    }

    private void Update() {
        if (Input.GetButtonDown("Shift")) {
            SwitchDevMode();
        }
    }

    void SwitchDevMode() {
        if (devUiPanel && Manager.dev) {
            Manager.dev = false;
            devUiPanel.SetActive(false);
        } else {
            Manager.dev = true;
            devUiPanel.SetActive(true);
        }
    }
}
