using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRodGameObject : ItemGameObject
{
    public AudioClip lightRodUseSound;

    public override void DoFunctionality()
    {
        canBePickedUp = false;

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

        UseSound();
        
        Debug.Log("LightRod.DoFunctionality()");
    }


    private void UseSound()
    {
        audioSource.clip = lightRodUseSound;
        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.Play();
    }
}
