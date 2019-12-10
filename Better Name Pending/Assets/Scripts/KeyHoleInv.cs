using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHoleInv : MonoBehaviour
{
    public Animation doorAnim;

    public void UnlockDoor(GameObject key)
    {
        if (key != null)
        {
            doorAnim.Play();
        }
    }
}
