﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class AudioManager 
{
    public static AudioMixer audioMixer;

    public enum AudioGroups
    {
        None,
        UIMusic,
        GameMusic,
        GameSFX,
        UISFX,
    }
    public static void PlaySound(AudioClip audioClipToPlay, AudioGroups audioGroups)
    {
        GameObject soundGameobject = new GameObject("Sound");
        AudioSource audioSource = soundGameobject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(audioClipToPlay);
        soundGameobject.GetComponent<AudioSource>().outputAudioMixerGroup = AudioManager.audioMixer.FindMatchingGroups(audioGroups.ToString())[0];
        GameObject.Destroy(soundGameobject, audioClipToPlay.length);
    }
}
