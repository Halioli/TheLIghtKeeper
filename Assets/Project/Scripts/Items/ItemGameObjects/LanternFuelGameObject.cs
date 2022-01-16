using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternFuelGameObject : ItemGameObject
{
    private Lamp playerLamp;
    private float lampTimeToRefill = 5f;

    public AudioClip lampFuelUseSound;

    private void FunctionalitySound()
    {
        audioSource.clip = lampFuelUseSound;
        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.Play();
    }

    public override void DoFunctionality()
    {
        canBePickedUp = false;
        playerLamp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Lamp>();

        if (playerLamp.CanRefill())
        {
            FunctionalitySound();
            playerLamp.RefillLampTime(lampTimeToRefill);
        }

        Destroy(gameObject);
    }
}
