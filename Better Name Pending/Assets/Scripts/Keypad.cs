using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Keypad : MonoBehaviour {
    public Color keyColor;
    public Color pressedKeyColor;
    public bool showGismoz;
    public int password;
    public int currentValue;
    public Text display;

    public void AddNumber(int number) {
        display.text = display.text + number;
        currentValue +
    }
}

