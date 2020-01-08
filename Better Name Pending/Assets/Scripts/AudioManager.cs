using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class AudioManager 
{
    public static void PlaySound(AudioClip audioClipToPlay, AudioMixer audioMixerToPlayFrom, string mixerGroup)
    {
        GameObject soundGameobject = new GameObject("Sound");
        AudioSource audioSource = soundGameobject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixerToPlayFrom.FindMatchingGroups(mixerGroup)[0];
        audioSource.PlayOneShot(audioClipToPlay);
        GameObject.Destroy(soundGameobject, audioClipToPlay.length);
    }
}
