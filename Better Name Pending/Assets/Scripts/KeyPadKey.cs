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
    public string keycardTag;
    public bool keycardReader;
    public AudioClip read;
    Keypad keyPad;
    public bool inRange;
    public Color inContact;
    public Color idle;
    public bool unlocked;
    public Material material;


    public delegate void NewTest();
    public NewTest newTest;


    void Test() {
        //ijsdoiparntaeruith
    }

    private void Start() {
        newTest = new NewTest(Test);
        keyPad = GetComponentInParent<Keypad>();
        if (material == null) {
            material = GetComponent<Renderer>().material;
        }
        if (keyPad) {
            ChangeColor(keyPad.keyColor);
            uiValue.color = keyPad.keyColor;
        } else {
            ChangeColor(idle);
        }
    }

    private void Update() {
        if (!unlocked) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            for (int i = 0; i < colliders.Length; i++) {
                if (!keycardReader) {
                    if (Array.Exists(colliders, element => element.transform.tag == fingerTag)) {
                        if (!inRange) {
                            ChangeColor(keyPad.pressedKeyColor);
                            uiValue.color = keyPad.pressedKeyColor;
                            keyPad.AddNumber(value);
                            AudioManager.PlaySound(keyPad.keyPress, AudioManager.AudioGroups.GameSFX);
                            inRange = true;
                        }
                    } else {
                        uiValue.color = keyPad.keyColor;
                        ChangeColor(keyPad.keyColor);
                        inRange = false;
                    }
                } else {
                    if (Array.Exists(colliders, element => element.transform.tag == keycardTag)) {
                        ChangeColor(inContact);
                        AudioManager.PlaySound(read, AudioManager.AudioGroups.GameSFX);
                        unlocked = true;
                    } else {
                        ChangeColor(idle);
                    }
                }
            }
        }
    }

    void ChangeColor(Color color) {
        material.SetColor("_EmissionColor", color);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}