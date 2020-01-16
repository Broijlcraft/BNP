using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifTranslator : MonoBehaviour
{
    public Gif gif;
    int currentFrame;
    public float refreshRate = 0.5f;
    Material targetMaterial;
    
    public void Start()
    {
        targetMaterial = GetComponent<Renderer>().material;
        InvokeRepeating("FrameUpdate", 0, refreshRate);
    }
    public void FrameUpdate()
    {
        if (gif) {
            if (gif.frames.Length > 0)
            {
                if (currentFrame < gif.frames.Length-1)
                {
                    currentFrame++;
                }
                else
                {
                    currentFrame = 0;
                }
                if (targetMaterial) {
                    targetMaterial.SetTexture("_BaseMap", gif.frames[currentFrame]);
                }
            } 
        }
    }
}
