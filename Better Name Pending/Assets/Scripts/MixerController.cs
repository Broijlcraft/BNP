using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider, sfxSlider, masterSlider;

    public void SetMusicVolume()
    {
        float sliderValue = musicSlider.value;
        audioMixer.SetFloat("musicVolume", sliderValue);
    }
    public void SetMasterVolume()
    {
        float sliderValue = masterSlider.value;
        audioMixer.SetFloat("masterVolume", sliderValue);
    }
    public void SetSFXVolume()
    {
        float sliderValue = sfxSlider.value;
        audioMixer.SetFloat("sfxVolume", sliderValue);
    }
}
