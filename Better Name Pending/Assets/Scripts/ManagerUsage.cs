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
        AudioManager.audioMixer = Resources.Load("MasterVolume") as AudioMixer;

        if (vrPlayer && pcPlayer) {
            if (XRDevice.isPresent) {
                pcPlayer.SetActive(false);
                vrPlayer.SetActive(true);
            } else {
                vrPlayer.SetActive(false);
                pcPlayer.SetActive(true);
            }
        }
    }

    private void Update() {
        if (Input.GetButtonDown("Shift")) {
            SwitchDevMode();
        }
    }

    void SwitchDevMode() {
        if (Manager.dev) {
            Manager.dev = false;
        } else {
            Manager.dev = true;
        }

        if (devUiPanel) {
            devUiPanel.SetActive(Manager.dev);
        }
    }
}
