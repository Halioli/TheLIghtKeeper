using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class BrokenFurnace : InteractStation
{
    private const int COAL_REPAIR_AMOUNT = 6;
    private PopUp popUp;
    private Animator furnaceAnimator;
    private Light2D furnaceLight;
    private AudioSource audioSource;

    public Light2D[] spaceShipLights;
    public Item coal;
    public HUDHandler hud;
    public ParticleSystem furnaceParticles;

    public delegate void BrokenFurnaceAction();
    public static event BrokenFurnaceAction OnTutorialFinish;

    private void Awake()
    {
        PlayerPrefs.SetInt("TutorialFinished", 0);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        popUp = GetComponentInChildren<PopUp>();
        furnaceAnimator = GetComponent<Animator>();

        furnaceAnimator.SetBool("isActivate", false);

        furnaceLight = GetComponentInChildren<Light2D>();

        for (int i = 1; i < spaceShipLights.Length; ++i)
        {
            spaceShipLights[i].intensity = 0f;
        }
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
            PlayerInputs.instance.canMove = false;

            StartCoroutine(FurnaceParticles());
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
        PlayerInputs.instance.canMove = false;

        // Play animation
        furnaceAnimator.SetBool("isActivate", true);

        yield return new WaitForSeconds(0.5f);

        //furnaceLight.intensity = 0.5f;
        //furnaceLight.gameObject.SetActive(false);
        //spaceShipLights[0].intensity = 0f;

        for(int i = 1; i < spaceShipLights.Length - 1; i++)
        {
            spaceShipLights[i].intensity = 1f;
            audioSource.Play();
            yield return new WaitForSeconds(1f);
        }
        for (int i = 0; i < spaceShipLights.Length - 1; i++)
        {
            //spaceShipLights[i].intensity = 0f;
            spaceShipLights[i].gameObject.SetActive(false);
        }
        spaceShipLights[spaceShipLights.Length - 1].intensity = 1f;
        audioSource.volume = 0.3f;
        audioSource.Play();
        yield return new WaitForSeconds(1f);

        // HUD fade to black
        hud.DoFadeToBlack();
        if (OnTutorialFinish != null) OnTutorialFinish();

        yield return new WaitForSeconds(2f);


        PlayerInputs.instance.canMove = true;

        // Load Scene
        SceneManager.LoadSceneAsync("Spaceship", LoadSceneMode.Single);
    }

    IEnumerator FurnaceParticles()
    {
        furnaceParticles.Play();
        yield return new WaitForSeconds(1f);
        furnaceParticles.Stop();
    }
}
