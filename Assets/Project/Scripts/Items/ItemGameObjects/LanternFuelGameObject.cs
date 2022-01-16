using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class LanternFuelGameObject : ItemGameObject
{
    private Lamp playerLamp;
    private float lampTimeToRefill = 5f;

    private float punchTime = 0.25f;

    public delegate void LanternFuelSound();
    public static event LanternFuelSound onLanternFuelRefill;



    private void FunctionalitySound()
    {
        if (onLanternFuelRefill != null)
            onLanternFuelRefill();
    }

    public override void DoFunctionality()
    {
        permanentNotPickedUp = true;
        canBePickedUp = false;

        playerLamp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Lamp>();

        if (playerLamp.CanRefill())
        {
            FunctionalitySound();
            playerLamp.RefillLampTime(lampTimeToRefill);
        }
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        transform.DOPunchScale(new Vector3(0.2f, 0.2f), punchTime);
        yield return new WaitForSeconds(punchTime);
        Destroy(gameObject);
    }
}
