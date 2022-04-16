using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStation : InteractStation
{
    //Private Atributes
    private float particleTime;
    private bool isOpen = false;
    [SerializeField] CraftingMenu craftingMenu;

    bool itemsWereSentToStorage = false;


    // Public Attributes
    public GameObject interactText;
    public GameObject backgroundText;
    public GameObject craftingCanvasGameObject;

    public ParticleSystem[] craftingParticles;


    public delegate void ItemSentToStorageMessegeAction();
    public static event ItemSentToStorageMessegeAction OnItemSentToStorage;



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
                CloseCraftingInventory();
            }
        }
    }

    private void OnEnable()
    {
        CraftingSystem.OnCrafting += PlayCraftingParticles;
        CraftingSystem.OnItemSentToStorage += () => itemsWereSentToStorage = true;

        CraftingStationAuxiliar.OnMenuOpen += OpenCraftingInventory;
        CraftingStationAuxiliar.OnMenuClose += CloseCraftingInventory;
    }

    private void OnDisable()
    {
        CraftingSystem.OnCrafting -= PlayCraftingParticles;
        CraftingSystem.OnItemSentToStorage -= () => itemsWereSentToStorage = true;

        CraftingStationAuxiliar.OnMenuOpen -= OpenCraftingInventory;
        CraftingStationAuxiliar.OnMenuClose -= CloseCraftingInventory;
    }

    //From InteractStation script
    public override void StationFunction()
    {
        if (isOpen)
        {
            CloseCraftingInventory();
        }
        else
        {
            OpenCraftingInventory();
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


    private void OpenCraftingInventory()
    {
        DoOnInteractOpen();
        DoOnInteractDescriptionOpen();

        isOpen = true;

        craftingCanvasGameObject.SetActive(true);
        craftingMenu.ShowRecepies();

        PlayerInputs.instance.canMine = false;
        //PauseMenu.gameIsPaused = true;
    }

    private void CloseCraftingInventory()
    {
        DoOnInteractClose();

        isOpen = false;

        craftingCanvasGameObject.SetActive(false);

        PlayerInputs.instance.canMine = true;
        //PauseMenu.gameIsPaused = false;


        if (itemsWereSentToStorage)
        {
            itemsWereSentToStorage = false;
            if (OnItemSentToStorage != null) OnItemSentToStorage();
        }
    }

}
