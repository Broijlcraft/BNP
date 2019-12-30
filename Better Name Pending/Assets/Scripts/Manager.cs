using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class VR_input {
    public string triggerInput;
    public string touchInput;
    public string gripInput;
}

public class Manager : MonoBehaviour {

    public VR_input rightHandInput;
    public VR_input leftHandInput;
    [Space]
    public float staticThrowMultiplier;
    public static float throwMultiplier;
    public float staticRotationMultiplier;
    public static float rotationMultiplier;

    private void Update() {
        throwMultiplier = staticThrowMultiplier;
        rotationMultiplier = staticRotationMultiplier;
    }
}
