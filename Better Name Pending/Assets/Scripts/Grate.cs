using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grate : CrowbarVersatilities {
    public override void CrowbarInteract()  {
        //for (int i = 0; i < objectsToDoSomethingOnInteract.Length; i++) {
        //    objectsToDoSomethingOnInteract[i].Interact();
        //}
        objectsToDoSomethingOnInteract[0].Interact();
        AudioManager.PlaySound(audioClip, audioGroups);
        gameObject.SetActive(false);
    }
}
