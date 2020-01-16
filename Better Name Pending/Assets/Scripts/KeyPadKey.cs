using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class KeyPadKey : MonoBehaviour {

    public Text uiValue;
    public int value;
    public Transform origin;
    public float range;
    public string fingerTag;
    Keypad keyPad;
    public bool fingerInRange;
    public Color inContact;
    public Color idle;
    [HideInInspector] public Material material;

    private void Start() {
        keyPad = GetComponentInParent<Keypad>();
        material = GetComponent<Renderer>().material;
        if (keyPad) {
            ChangeColor(keyPad.keyColor);
            uiValue.color = keyPad.keyColor;
        } else {
            ChangeColor(idle);
        }
    }

    private void Update() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        for (int i = 0; i < colliders.Length; i++) {
            if (Array.Exists(colliders, element => element.transform.tag == fingerTag)) {
                if (!fingerInRange) {
                    ChangeColor(keyPad.pressedKeyColor);
                    uiValue.color = keyPad.pressedKeyColor;
                    AudioManager.PlaySound(keyPad.keyPress, AudioManager.AudioGroups.GameSFX);
                    keyPad.AddNumber(value);
                    fingerInRange = true;
                }
            } else {
                uiValue.color = keyPad.keyColor;
                ChangeColor(keyPad.keyColor);
                fingerInRange = false;
            }
        }
    }

    void ChangeColor(Color color) {
        material.SetColor("_EmissionColor", color);
    }

    private void OnDrawGizmos() {
        if (keyPad && keyPad.showGismoz) {
            Gizmos.DrawWireSphere(transform.position, range);
        }        
    }
}