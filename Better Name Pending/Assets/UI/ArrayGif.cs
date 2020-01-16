using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayGif : MonoBehaviour
{
    public Texture2D[] frames;
    public int framesPerSecond;

    public void Update()
    {
        int index = (int) Time.time * framesPerSecond;
        index = index % frames.Length;
        GetComponent<Material>().mainTexture = frames[index];
    }
}
