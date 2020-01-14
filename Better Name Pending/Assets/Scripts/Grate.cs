using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grate : CrowbarVersatilities
{
    public override void CrowbarInteract() 
    {
        AudioManager.PlaySound(audioClip, audioGroups);
        gameObject.GetComponent<Animation>().Play();
    }
}
