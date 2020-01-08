using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Audio;

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
        AudioManager.audioMixer = Resources.Load("Master Volume") as AudioMixer;
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
