using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInteractable : MonoBehaviour {

    public GameObject[] objects;
    
    public virtual void Interact() {
        SetActiveOnInteract();
    }


    public virtual void SetActiveOnInteract() {
        for (int i = 0; i < objects.Length; i++) {
            objects[i].SetActive(true);
        }
    }
}
