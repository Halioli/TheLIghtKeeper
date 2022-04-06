using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OreVein : InteractStation
{
    // Public Attributes
    public Item materialVein;
    public Item autoMiner;
    public GameObject autoMinerGameObject;
    public PopUp popUp;
    public ParticleSystem[] veinParticlesSystems;

    // Private Attributes
    private string[] messagesToShow = { "", "No Auto-Miner found" };
    private bool activated = false;

    private void Start()
    {
        autoMinerGameObject.SetActive(activated);
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
        if (playerInventory.InventoryContainsItem(autoMiner))
        {
            activated = true;

            playerInventory.SubstractItemFromInventory(autoMiner);
            popUp.ChangeMessageText(messagesToShow[0]);
            StartCoroutine(VeinParticleSystem());
            autoMinerGameObject.SetActive(true);

            autoMinerGameObject.GetComponent<AutoMiner>().GetsPlacedDown(materialVein);
        }
        else
        {
            popUp.ChangeMessageText(messagesToShow[1]);

            InvokeOnNotEnoughMaterials();
        }
    }

    private IEnumerator VeinParticleSystem()
    {
        foreach (ParticleSystem system in veinParticlesSystems)
        {
            system.Play();
        }
        yield return new WaitForSeconds(1f);
        foreach (ParticleSystem system in veinParticlesSystems)
        {
            system.Stop();
        }
    }
}
