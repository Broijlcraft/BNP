using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUsage : MonoBehaviour {

    public GameObject devUiPanel;

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
