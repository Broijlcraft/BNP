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

public class VrInputManager : MonoBehaviour {
    
    public VR_input rightHandInput;
    public VR_input leftHandInput;
    public enum Pickup {
        toggle,
        hold
    }
    [Space]
    public Pickup pickup;
    public float staticThrowMultiplier;
    public static float throwMultiplier;
    public float staticRotationMultiplier;
    public static float rotationMultiplier;

    private void Update() {
        throwMultiplier = staticThrowMultiplier;
        rotationMultiplier = staticRotationMultiplier;
    }
}
