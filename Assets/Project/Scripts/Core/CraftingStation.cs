using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStation : InteractStation
{
    //Private Atributes
    private float particleTime;

    // Public Attributes
    public GameObject interactText;
    public GameObject craftingCanvasGameObject;
    public GameObject playerHUDGameObject;

    public InventoryMenu inventoryMenu;
    public ParticleSystem[] craftingParticles;

    private void Start()
    {
        foreach (ParticleSystem particle in craftingParticles)
        {
            particle.Stop();
        }

        particleTime = 1.89f;
    }
    void Update()
    {
        // If player enters the trigger area the interactionText will appears
        if (playerInsideTriggerArea)
        {
            GetInput();            //Waits the input from interactStation 
            PopUpAppears();
        }
        else
        {
            PopUpDisappears();
            if (craftingCanvasGameObject.activeInHierarchy)
            {
                playerHUDGameObject.SetActive(true);
                craftingCanvasGameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        CraftingSystem.OnCrafting += PlayCraftingParticles;
    }

    private void OnDisable()
    {
        CraftingSystem.OnCrafting -= PlayCraftingParticles;
    }

    //From InteractStation script
    public override void StationFunction()
    {
        if (!craftingCanvasGameObject.activeInHierarchy)
        {
            playerHUDGameObject.SetActive(false);
            craftingCanvasGameObject.SetActive(true);
            inventoryMenu.UpdateInventory();
            PauseMenu.gameIsPaused = true;
        }
        else
        {
            playerHUDGameObject.SetActive(true);
            craftingCanvasGameObject.SetActive(false);
            PauseMenu.gameIsPaused = false;
        }
    }

    //Interactive pop up disappears
    private void PopUpAppears()
    {
        interactText.SetActive(true);
    }

    //Interactive pop up disappears
    private void PopUpDisappears()
    {
        interactText.SetActive(false);
    }

    private void PlayCraftingParticles()
    {
        StartCoroutine(CraftingParticleSystem());
    }

    IEnumerator CraftingParticleSystem()
    {
        foreach (ParticleSystem particle in craftingParticles)
        {
            particle.Play();
        }

        yield return new WaitForSeconds(particleTime);

        foreach (ParticleSystem particle in craftingParticles)
        {
            particle.Stop();
        }
    }
}
