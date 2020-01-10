using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadKey : MonoBehaviour {

    public int value;
    public Transform origin;
    public float range;
    Keypad keyPad;
    [HideInInspector] public Material material;

    private void Start() {
        keyPad = GetComponentInParent<Keypad>();
        material = GetComponent<Renderer>().material;
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            if (material) {
                //material.EnableKeyword("_EmissionColor");
                print("Mat");
                material.SetColor("_EmissionColor", keyPad.keyColor);
            } else {
                print("No Mat");
            }
        }
    }
    private void OnDrawGizmos() {
        if (keyPad && keyPad.showGismoz) {
            if (origin) {
                Gizmos.DrawWireSphere(origin.position, range);
            } else {
                Gizmos.DrawWireSphere(transform.position, range);
            }
        }        
    }
}