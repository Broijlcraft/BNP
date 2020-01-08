using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider, sfxSlider, masterSlider;

     public static MixerController instance;
        
    public void Awake()
    {
        if(MixerController.instance == null)
        {
            DontDestroyOnLoad(gameObject);
            MixerController.instance = this;
        }
       else
       {
            Destroy(gameObject);
       }
    }

    public void Start() 
    {
        
    }

    public void SetMusicVolume()
    {
        float sliderValue = musicSlider.value;
        audioMixer.SetFloat("musicVolume", sliderValue);
        PlayerPrefs.SetFloat("musicVolume", sliderValue);
        PlayerPrefs.Save();
    }
    public void SetMasterVolume()
    {
        float sliderValue = masterSlider.value;
        audioMixer.SetFloat("masterVolume", sliderValue);
        PlayerPrefs.SetFloat("masterVolume", sliderValue);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume()
    {
        float sliderValue = sfxSlider.value;
        audioMixer.SetFloat("sfxVolume", sliderValue);
        PlayerPrefs.SetFloat("sfxVolume", sliderValue);
        PlayerPrefs.Save();
    }
}
