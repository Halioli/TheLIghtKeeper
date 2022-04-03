using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFisure : InteractStation
{
    // Public Attributes
    [SerializeField] Item lightGenerator;
    [SerializeField] GameObject lightGeneratorGameObject;
    [SerializeField] GameObject coneLightGameObject;
    [SerializeField] ConeLight coneLight;
    [SerializeField] PopUp popUp;

    // Private Attributes
    private string[] messagesToShow = { "", "No <b>Light Generator</b> found" };
    private bool activated = false;
    private bool inCoroutine = false;

    private void Start()
    {
        lightGeneratorGameObject.SetActive(activated);
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
            lightGeneratorGameObject.SetActive(true);

            StopCoroutine(LightFisureFlicker());
            coneLightGameObject.SetActive(false);
            popUp.gameObject.SetActive(false);
        }
        else
        {
            popUp.ChangeMessageText(messagesToShow[1]);

            InvokeOnNotEnoughMaterials();
        }
    }

    private IEnumerator LightFisureFlicker()
    {
        inCoroutine = true;

        coneLight.SetIntensity(Random.Range(0.8f, 1.0f));

        yield return new WaitForSeconds(0.1f);

        inCoroutine = false;
    }
}
