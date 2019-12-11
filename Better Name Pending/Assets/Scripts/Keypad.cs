using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    public static string codePassword;
    public static int currentValue;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Hand")
        {
            currentValue = currentValue + '1';
        }
    }
}
