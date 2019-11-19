using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{
    private Transform player;

    public GameObject item;
    public Transform keyhole;

    public GameObject key;
    public GameObject keyPrefab;
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
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;

        if (item.tag == "Key")
        {
            float distance = Vector3.Distance(keyhole.position, transform.position);

            if (distance <= minDistance)
            {
                Instantiate(key, keyhole.position, Quaternion.identity);
                keyhole.GetComponent<KeyHoleInv>().key = keyPrefab;
                Destroy(item);
            }

            if (Physics.Raycast(player.position, player.forward, out RaycastHit hit, max))
            {
                Debug.DrawRay(player.position, player.forward, Color.red);
                if (hit.transform.tag == "KeyHole")
                {
                    if (Input.GetButtonDown("Interact") && hit.transform.GetComponent<KeyHoleInv>().key != null)
                    {
                        Instantiate(hit.transform.GetComponent<KeyHoleInv>().key, transform);
                        hit.transform.GetComponent<KeyHoleInv>().key = null;
                        Debug.Log("Got key back");
                    }
                }
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
