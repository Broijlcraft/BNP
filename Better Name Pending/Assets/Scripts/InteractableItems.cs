using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class InteractableItems : MonoBehaviour
{
    public GameObject item;
    public Transform keyhole;
    public GameObject keyPrefab;

    public GameObject key;
    public float minDistance;

    public Rigidbody rb;
    public float minVelocity;

    private float max;

    public void Start()
    {
        item = gameObject;
        max = 15f;
    }

    public void Update()
    {
        if (item.tag == "Key")
        {
            float distance = Vector3.Distance(keyhole.position, transform.position);

            if (distance <= minDistance)
            {
                keyhole.transform.GetComponent<KeyHoleInv>().key = Instantiate(keyPrefab, keyhole.position, Quaternion.identity);
                Destroy(item, 0.1f);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {
            return;
        }   

        if (collision.gameObject.tag == "Window")
        {
            if (rb.velocity.magnitude >= minVelocity && item.tag == "Crowbar")
            {
                BreakWindow();
            }
        }
    }

    private void BreakWindow()
    {
        Debug.Log("Ik ben een gebroken raam");
    }
}
//TOFIX:
//Take key out.
