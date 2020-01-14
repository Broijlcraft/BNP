using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : CrowbarVersatilities {

    public override void CrowbarInteract() {
        AudioManager.PlaySound(audioClip, audioGroups);
        gameObject.GetComponent<Animation>().Play();
    }
}
