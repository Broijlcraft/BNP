using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportArea : MonoBehaviour {

    public bool teleportable;
    public Color green = new Vector4(0f, 1f, 0.2f, 0.2f);
    public Color red = new Vector4(1f, 0f, 0f, 0.6f);

    public Color notSelected;

    Vector4 currentColor;

    Renderer tpRenderer;

    private void Start() {
        tpRenderer = GetComponent<Renderer>();
    }

    private void Update() {
        TeleportTest();
    }

    public void PointerInteract() {

    }

    public void TeleportTest() {
        if (teleportable == true) {
            currentColor = green;
        } else {
            currentColor = red;
        }
        tpRenderer.material.SetColor("_BaseColor", currentColor);
    }
}
