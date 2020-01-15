using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grate : CrowbarVersatilities
{
    private void Start() {
        CrowbarInteract();
    }
    public override void CrowbarInteract() 
    {
        AudioManager.PlaySound(audioClip, audioGroups);
        gameObject.GetComponent<Animation>().Play();
    }
}
