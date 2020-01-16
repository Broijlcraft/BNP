using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportArea : Hands {

    public bool teleportable;
    public Color green = new Vector4(0f, 1f, 0.2f, 0.2f);
    public Color red = new Vector4(1f, 0f, 0f, 0.6f);

    public Renderer tpRenderer;
    bool hasBeenDown;

    private void Start() {
        tpRenderer.enabled = false;
        tpRenderer.material.SetColor("_BaseColor", green);
        SetVrInputs();
    }

    private void Update() {
        if (MouseInputAndVRAxisCheck(2, touchInput, "Useless_Input") == true) {
            if (!hasBeenDown) {
                TeleportTest(true);
                hasBeenDown = true;
            }
        } else {
            if (hasBeenDown) {
                TeleportTest(false);
                hasBeenDown = false;
            }
        }
    }

    public void TeleportTest(bool down) {
        tpRenderer.enabled = down;
    }
}
