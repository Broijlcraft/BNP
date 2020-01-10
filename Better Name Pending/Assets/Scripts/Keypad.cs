using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Keypad : MonoBehaviour {
    public Color keyColor;
    public Color pressedKeyColor;
    public float resetDelay;
    public AudioClip keyPress;
    public AudioClip wrongCode;
    public AudioClip unlockDoor;
    public bool showGismoz;
    public int passcode;
    public Text display;
    int currentValue;
    bool cooldown;

    private void Start() {
        ResetValues();
    }

    public void AddNumber(int number) {
        if(currentValue < 999 && !cooldown) {
            display.color = keyColor;
            currentValue = currentValue * 10;
            currentValue += number;
            display.text = currentValue.ToString();
            if(currentValue > 999) {
                cooldown = true;
                if (currentValue == passcode) {
                    AudioManager.PlaySound(unlockDoor, AudioManager.AudioGroups.GameSFX);
                } else {
                    Invoke("ResetValues", resetDelay);
                    display.color = pressedKeyColor;
                    AudioManager.PlaySound(wrongCode, AudioManager.AudioGroups.GameSFX);
                }
            }
        }
    }

    void ResetValues() {
        cooldown = false;
        currentValue = 0;
        display.text = "";
    }
}