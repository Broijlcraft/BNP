using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractableItems
{
    public Transform keyHole;
    public float minDistance;
    public bool hasInserted;

    private void Update()
    {
        float distance = Vector3.Distance(keyHole.position, transform.position);

        if (hasInserted)
        {
            if (keyHole.GetComponentInParent<KeyHoleInv>().doorAnim.isPlaying == true)
            {
                transform.position = keyHole.position;
                transform.LookAt(keyHole);
            }
        }

        else if (distance <= minDistance)
        {
            hasInserted = true;
            keyHole.GetComponentInParent<KeyHoleInv>().UnlockDoor(gameObject);
        }
    }
}
