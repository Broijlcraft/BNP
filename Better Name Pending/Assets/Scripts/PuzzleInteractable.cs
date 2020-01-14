using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInteractable : MonoBehaviour {

    public GameObject wine;

    public virtual void Interact() {
        wine.SetActive(true);
    }
}
