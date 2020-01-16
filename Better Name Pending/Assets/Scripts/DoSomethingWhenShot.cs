using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoSomethingWhenShot : MonoBehaviour {
    public GameObject bulletHole;
    public virtual void GetShot(Vector3 position, Vector3 rotation/*must be vector3.normal value*/) {
        GameObject hole = Instantiate(bulletHole, position, Quaternion.FromToRotation(Vector3.up, rotation));
        hole.transform.SetParent(transform);
    }
}
