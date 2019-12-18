using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager 
{
    public static void PlaySound(AudioClip audioClipToPlay){
        GameObject soundGameobject = new GameObject("Sound");
        AudioSource audioSource = soundGameobject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(audioClipToPlay);
    }
}
