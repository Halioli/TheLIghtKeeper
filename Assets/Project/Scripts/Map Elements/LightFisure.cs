using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class LightFisure : InteractStation
{
    // Public Attributes
    [SerializeField] Item lightGenerator;
    [SerializeField] GameObject lightGeneratorGameObject;
    [SerializeField] GameObject auxiliarCraftingStation;

    [SerializeField] ConeLightCircleInteriorLight coneLight;  
    [SerializeField] CircleCollider2D coneLightCollider1;
    [SerializeField] CircleCollider2D coneLightCollider2;

    [SerializeField] PopUp popUp;

    [SerializeField] Transform spriteTransform;
    [SerializeField] Transform craftingSpriteTransform;
    [SerializeField] AudioSource audioSource;


    // Private Attributes
    private string[] messagesToShow = { "", "No <b>Light Generator</b> found" };
    private bool activated = false;
    private bool inCoroutine = false;

    private void Start()
    {
        lightGeneratorGameObject.SetActive(activated);
        auxiliarCraftingStation.SetActive(activated);

        coneLightCollider1.enabled = false;
        coneLightCollider2.enabled = false;
    }

    void Update()
    {
        if (playerInsideTriggerArea)
        {
            GetInput();
            PopUpAppears();
        }
        else
        {
            PopUpDisappears();
        }

        if (!activated && !inCoroutine)
        {
            StartCoroutine(LightFisureFlicker());
        }
    }

    // Interactive pop up disappears
    private void PopUpAppears()
    {
        popUp.ShowInteraction();
    }

    // Interactive pop up disappears
    private void PopUpDisappears()
    {
        popUp.HideAll();
        popUp.ChangeMessageText(messagesToShow[0]);
    }

    public override void StationFunction()
    {
        if (activated) return;

        popUp.ShowMessage();
        if (playerInventory.InventoryContainsItem(lightGenerator))
        {
            activated = true;

            playerInventory.SubstractItemFromInventory(lightGenerator);
            popUp.ChangeMessageText(messagesToShow[0]);
            lightGeneratorGameObject.SetActive(activated);
            auxiliarCraftingStation.SetActive(activated);
            
            coneLight.SetDistance(10f);
            coneLight.ExtraExpand(400, 400, 1.0f);
            coneLightCollider1.enabled = true;
            coneLightCollider2.enabled = true;

            StopCoroutine(LightFisureFlicker());
            //coneLightGameObject.SetActive(false);
            popUp.gameObject.SetActive(false);

            PlacedAnimation();
            PlayPlacedSound();
        }
        else
        {
            //popUp.ChangeMessageText(messagesToShow[1]);

            InvokeOnNotEnoughMaterials();
        }
    }

    private IEnumerator LightFisureFlicker()
    {
        inCoroutine = true;

        coneLight.SetIntensity(Random.Range(0.2f, 0.3f));

        yield return new WaitForSeconds(0.1f);

        inCoroutine = false;
    }



    private void PlacedAnimation()
    {
        spriteTransform.DOPunchScale(new Vector3(0.1f, 0.4f), 0.5f, 10, 10);
        craftingSpriteTransform.DOPunchScale(new Vector3(0.1f, 0.4f), 0.5f, 10, 10);
    }

    private void PlayPlacedSound()
    {
        //audioSource.clip = placeSound;
        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.Play();
    }




}
