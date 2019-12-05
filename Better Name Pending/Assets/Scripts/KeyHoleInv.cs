using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHoleInv : MonoBehaviour
{
    public GameObject key;

    public void Update()
    {
        UnlockDoor();
    }

    private void UnlockDoor()
    {
        if (key != null)
        {
            //Enable Collider for hand to grab. 
            //Enable open the door.
        }
    }
}
