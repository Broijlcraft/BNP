using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider, sfxSlider;

    public void SetMusicVolume()
    {
        float sliderValue = musicSlider.value;
        audioMixer.SetFloat("musicVolume", sliderValue);
    }
    public void SetSFXVolume()
    {
        float sliderValue = musicSlider.value;
        audioMixer.SetFloat("sfxVolume", sliderValue);
    }
}
