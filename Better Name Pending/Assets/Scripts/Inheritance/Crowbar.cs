using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : InteractableItems
{
    private Animation boxAnim;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            boxAnim = collision.gameObject.GetComponent<Animation>();
            BreakBox();
            BreakItem();
        }    
    }

    private void BreakBox()
    {
        boxAnim.Play();
    }

    private void BreakItem()
    {
        AudioManager.PlaySound(audioClip, AudioManager.AudioGroups.GameSFX);
        Destroy(gameObject, 0.1f);
        for (int i = 0; i < brokenItems.Length; i++)
        {
            Instantiate(brokenItems[i], transform.position, transform.rotation);
        }
    }
}
