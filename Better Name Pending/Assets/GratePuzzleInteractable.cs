using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GratePuzzleInteractable : PuzzleInteractable {
    public override void Interact() {
        GetComponent<Interactable>().blockPickUp = false;
        transform.SetParent(null);
    }
}
