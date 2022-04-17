using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBroken : InteractStation
{
    private ConstuctionPopUp constuctionPopUp;

    public ParticleSystem[] bridgeParticleSytems;

    [SerializeField] BridgeManager bridgeManager;
    [SerializeField] int[] requiredAmount;
    [SerializeField] Item[] requiredMaterials;

    bool finishedConstruct;
    private void Start()
    {
        constuctionPopUp = GetComponentInChildren<ConstuctionPopUp>();

    }

    private void Update()
    {
        // If player enters the trigger area the interactionText will appears
        if (playerInsideTriggerArea)
        {
            // Waits the input from interactStation
            GetInput();
            PopUpAppears();
        }
        else
        {
            PopUpDisappears();
        }
    }

    // From InteractStation script
    public override void StationFunction()
    {
        if (HasRequiredMaterials())
        {
            constuctionPopUp.ChangeMessageText("Materials added");
            RemoveItemsFromInventory();

            constuctionPopUp.SetAllValue(0);

            StartCoroutine(BridgeParticleSystem());
        }
        else
        {
            constuctionPopUp.ChangeMessageText("Not enough materials");

            InvokeOnNotEnoughMaterials();            
        }
    }

    private bool HasRequiredMaterials()
    {
        bool hasEnough = false;

        if (requiredMaterials.Length == 0)
        {
            hasEnough = true;
        }
        else
        {
            for (int i = 0; i < requiredMaterials.Length; i++)
            {
                if (playerInventory.InventoryContainsItemAndAmount(requiredMaterials[i], requiredAmount[i]))
                {
                    hasEnough = true;
                }
                else
                {
                    hasEnough = false;
                    i = requiredMaterials.Length;
                }
            }
        }

        return hasEnough;
    }

    private void RemoveItemsFromInventory()
    {
        if (requiredMaterials.Length == 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < requiredMaterials.Length; i++)
            {
                playerInventory.SubstractNItemsFromInventory(requiredMaterials[i], requiredAmount[i]);
            }
        }
    }

    // Interactive pop up disappears
    private void PopUpAppears()
    {
        constuctionPopUp.ShowAll();
    }

    // Interactive pop up disappears
    private void PopUpDisappears()
    {
        constuctionPopUp.HideAll();
        constuctionPopUp.ChangeMessageText("Press E to interact");
    }

    private IEnumerator BridgeParticleSystem()
    {
        foreach (ParticleSystem particles in bridgeParticleSytems)
        {
            particles.Play();
        }
        bridgeManager.audioSource.Play();
        yield return new WaitForSeconds(2.5f);

        foreach (ParticleSystem particles in bridgeParticleSytems)
        {
            particles.Stop();
        }
        bridgeManager.BridgeConstructed();
        bridgeManager.constructed = true;
    }
}
