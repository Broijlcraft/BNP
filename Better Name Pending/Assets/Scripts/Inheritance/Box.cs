using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioManager.AudioGroups audioGroups;
    public void BoxBreak()
    {

        AudioManager.PlaySound(audioClip, audioGroups);
        gameObject.GetComponent<Animation>().Play();
    }
}
