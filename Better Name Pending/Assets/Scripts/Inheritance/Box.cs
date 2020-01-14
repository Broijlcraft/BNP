using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : CrowbarVersatilities 
{
    public override void CrowbarInteract() 
    {
        AudioManager.PlaySound(audioClip, audioGroups);
        gameObject.GetComponent<Animation>().Play();
        for(int i = 0; i < objectsToDoSomethingOnInteract.Length; i++) {
            objectsToDoSomethingOnInteract[i].Interact();
        }
    }
}
