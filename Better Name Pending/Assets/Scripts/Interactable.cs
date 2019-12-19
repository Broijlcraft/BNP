using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public Vector3 setPosition;
    public Vector3 setRotation;

    public virtual void Use() {
        //print("Base use");
    }    
}
