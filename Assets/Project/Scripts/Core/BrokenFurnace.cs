using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrokenFurnace : InteractStation
{
    private const int COAL_REPAIR_AMOUNT = 6;
    private PopUp popUp;
    private Animator furnaceAnimator;

    public Item coal;
    public HUDHandler hud;

    public delegate void BrokenFurnaceAction();
    public static event BrokenFurnaceAction OnTutorialFinish;


    private void Start()
    {
        popUp = GetComponentInChildren<PopUp>();
        furnaceAnimator = GetComponent<Animator>();

        furnaceAnimator.SetBool("isActivate", false);
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
            //PopUpDisappears();
            popUp.ChangeMessageText("Press E to interact");
        }
    }

    // From InteractStation script
    public override void StationFunction()
    {
        // Check if player has enough items
        if (playerInventory.InventoryContainsItemAndAmount(coal, COAL_REPAIR_AMOUNT))
        {
            popUp.ChangeMessageText("Materials added");
            playerInventory.SubstractNItemsFromInventory(coal, COAL_REPAIR_AMOUNT);

            StartCoroutine(StartFurnace());
        }
        else
        {
            popUp.ChangeMessageText("Not enough materials");

            InvokeOnNotEnoughMaterials();
        }
    }

    // Interactive pop up disappears
    private void PopUpAppears()
    {
        popUp.ShowAll();
    }

    // Interactive pop up disappears
    private void PopUpDisappears()
    {
        popUp.HideAll();
        popUp.ChangeMessageText("Press E to interact");
    }

    IEnumerator StartFurnace()
    {
        // Play animation
        furnaceAnimator.SetBool("isActivate", true);

        yield return new WaitForSeconds(2f);

        // HUD fade to black
        hud.DoFadeToBlack();
        if (OnTutorialFinish != null) OnTutorialFinish();

        yield return new WaitForSeconds(2f);

        // Load Scene
        SceneManager.LoadSceneAsync("Spaceship", LoadSceneMode.Single);
    }
}
