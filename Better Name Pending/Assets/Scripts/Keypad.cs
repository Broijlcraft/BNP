using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Keypad : MonoBehaviour {
    public Color keyColor;
    public Color pressedKeyColor;
    public float resetDelay;
    public AudioClip keyPress, wrongCode, unlockDoor;
    public bool showGismoz;
    public int passcode;
    public Text display;
    public Door door;
    [HideInInspector] public int currentValue;
    [HideInInspector] public bool cooldown, unlocked;

    private void Start() {
        ResetValues();
    }

    public void AddNumber(int number) {
        if(currentValue < 1000 && !cooldown) {
            display.color = keyColor;
            currentValue = currentValue * 10;
            currentValue += number;
            display.text = currentValue.ToString();
            if(currentValue > 999) {
                cooldown = true;
                if (currentValue == passcode) {
                    unlocked = true;
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