using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public Text keypadText;
    public int valueToGive;
    public int currentKeypadText;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Hand")
        {
            KeypadScreen(valueToGive);
        }
    }

    public void KeypadScreen(int value)
    {
        print(currentKeypadText + "" + value);
    }
}
