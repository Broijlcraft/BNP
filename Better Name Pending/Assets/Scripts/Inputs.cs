using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inputs : ScriptableObject {
    public bool pressed;
    public string no_inputName;

    [System.Serializable]
    public class Actual_Input {
        public string s;
    }
    public Actual_Input actual_Input;

    [System.Serializable]
    public class A {
        public string s;
    }
    public A a;
}
