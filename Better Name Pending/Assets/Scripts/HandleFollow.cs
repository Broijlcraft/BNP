using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleFollow : MonoBehaviour {

    public Transform handleCollider;

    private void FixedUpdate() {
        transform.localPosition = handleCollider.localPosition;
    }
}
