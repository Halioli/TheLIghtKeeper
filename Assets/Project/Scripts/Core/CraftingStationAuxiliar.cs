using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStationAuxiliar : InteractStation
{
    //Private Atributes
    private float particleTime;
    private bool isOpen = false;

    // Public Attributes
    public GameObject canvasCrafting;

    public ParticleSystem[] craftingParticles;

    public bool isUsingThisAuxiliar = false;


    [SerializeField] Animator animator;


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
            if (!isOpen)
            {
                PopUpDisappears();

                if (isUsingThisAuxiliar)
                {
                    if (OnMenuClose != null) OnMenuClose();
                }  
            }
        }

    }

    private void OnEnable()
    {
        CraftingSystem.OnCrafting += PlayCraftingParticles;
        CraftingStation.OnCraftAnimationPlay += PlayCraftAnimation;
    }

    private void OnDisable()
    {
        CraftingSystem.OnCrafting -= PlayCraftingParticles;
        CraftingStation.OnCraftAnimationPlay -= PlayCraftAnimation;
    }

    //From InteractStation script
    public override void StationFunction()
    {
        if (isOpen)
        {
            isUsingThisAuxiliar = false;
            if (OnMenuClose != null) OnMenuClose();
        }
        else
        {
            isUsingThisAuxiliar = true;
            if (OnMenuOpen != null) OnMenuOpen();
        }

        isOpen = !isOpen;
    }

    //Interactive pop up disappears
    private void PopUpAppears()
    {
        canvasCrafting.SetActive(true);
    }

    //Interactive pop up disappears
    private void PopUpDisappears()
    {
        canvasCrafting.SetActive(false);
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



    void PlayCraftAnimation()
    {
        animator.SetTrigger("craft");
    }

}
