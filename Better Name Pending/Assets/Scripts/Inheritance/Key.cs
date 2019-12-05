using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractableItems
{
    public Transform keyHole;
    public GameObject keyPrefab;
    public float minDistance;

    private void Update()
    {
        float distance = Vector3.Distance(keyHole.position, transform.position);

        if (distance <= minDistance)
        {
            keyHole.transform.GetComponent<KeyHoleInv>().key = Instantiate(keyPrefab, keyHole.position, Quaternion.identity);
            Destroy(gameObject, 0.1f);
        }
    }
}
