using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class AudioManager 
{
    public static AudioMixer audioMixer;

    public enum AudioGroups{
        UIMusic,
        GameMusic,
        GameSFX,
        UISFX,
    }
    public static void PlaySound(AudioClip audioClipToPlay, AudioMixer audioMixerToPlayFrom, AudioGroups audioGroups)
    {
        GameObject soundGameobject = new GameObject("Sound");
        AudioSource audioSource = soundGameobject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(audioClipToPlay);
        soundGameobject.GetComponent<AudioSource>().outputAudioMixerGroup = audioMixerToPlayFrom.FindMatchingGroups(subPath: audioGroups.ToString())[0];
        GameObject.Destroy(soundGameobject, audioClipToPlay.length);
    }
}
