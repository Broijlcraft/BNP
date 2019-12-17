using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Interactable {

    private void Start() {
        use = Shoot;
    }

    public void Shoot() {
        print("Shoot");
    }
}
