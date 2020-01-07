using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class AudioManager 
{
    public static void PlaySound(AudioClip audioClipToPlay)
    {
        GameObject soundGameobject = new GameObject("Sound");
        AudioSource audioSource = soundGameobject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(audioClipToPlay);
        GameObject.Destroy(soundGameobject, audioClipToPlay.length);
    }
}
