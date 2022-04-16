using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStationAuxiliar : InteractStation
{
    //Private Atributes
    private float particleTime;
    private bool isOpen = false;

    // Public Attributes
    public GameObject interactText;
    public GameObject backgroundText;

    public ParticleSystem[] craftingParticles;


    public delegate void CraftingStationAuxiliarAction();
    public static event CraftingStationAuxiliarAction OnMenuOpen;
    public static event CraftingStationAuxiliarAction OnMenuClose;



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
            if (OnMenuClose != null) OnMenuClose();
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
        if (isOpen)
        {
            if (OnMenuClose != null) OnMenuClose();
        }
        else
        {
            if (OnMenuOpen != null) OnMenuOpen();
        }
    }

    //Interactive pop up disappears
    private void PopUpAppears()
    {
        interactText.SetActive(true);
        backgroundText.SetActive(true);
    }

    //Interactive pop up disappears
    private void PopUpDisappears()
    {
        interactText.SetActive(false);
        backgroundText.SetActive(false);
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
