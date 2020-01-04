using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BlackLightSource : MonoBehaviour
{
    public Material reveal;
    public Light blackLight;
    void Update()
    {
        reveal.SetVector("_LightPosition", blackLight.transform.position);
        reveal.SetVector("_LightDirection", -blackLight.transform.forward);
        reveal.SetFloat("_LightAngle", blackLight.spotAngle);
    }
}
