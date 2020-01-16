using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gif : MonoBehaviour
{
    public Texture[] frames;
    public int currentGif;
    public float refreshRate;
    public Material renderTexture;
    public Material materiall;
    public void Start()
    {
        materiall = GetComponent<Renderer>().material;
        InvokeRepeating("FrameUpdate", 0, refreshRate);
    }
    public void FrameUpdate()
    {
        print(currentGif);
        if (frames.Length > 0)
        {
            if (currentGif < frames.Length-1)
            {
                currentGif++;
            }
            else
            {
                currentGif = 0;
            }
            materiall.SetTexture("_BaseMap", frames[currentGif]);
            //renderTexture.mainTexture = frames[currentGif];
        } 
    }
}
