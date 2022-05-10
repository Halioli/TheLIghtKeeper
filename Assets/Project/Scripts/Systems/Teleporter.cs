using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Teleporter : InteractStation
{
    // Private Attributes
    private Vector2 spawnPosition;
    public Animator animator;
    private bool updatedSystem = false;

    // Public Attributes
    //Station
    public PopUp popUp;
    public GameObject canvasTeleportSelection;
    public GameObject hudGameObject;
    public TextMeshProUGUI mssgText;
    public Inventory inventory;

    //Teleport
    public Item darkEssence;
    public string teleportName;
    public Vector3 teleportTransformPosition;
    public bool activated;
    public GameObject teleportSprite;
    public GameObject teleportLight;
    public SpriteRenderer teleportSpriteRenderer;
    public Sprite teleportActivatedSprite;

    [SerializeField] AudioSource teleportAudioSource;

    //Events / Actions
    public delegate void TeleportActivation(string teleportName);
    public static event TeleportActivation OnActivation;

    public delegate void TeleportInteraction(string teleportName);
    public static event TeleportInteraction OnInteraction;


    public delegate void TeleportMenuAction();
    public static event TeleportMenuAction OnMenuEnter;
    public static event TeleportMenuAction OnMenuExit;

    void Awake()
    {
        SaveSystem.teleporters.Add(this);
        teleportTransformPosition = GetComponent<Transform>().position;
        teleportTransformPosition.y -= 1.3f;
        spawnPosition = transform.position;
        animator = GetComponent<Animator>();
        teleportSprite.SetActive(true);
        teleportLight.SetActive(false);
    }

    private void Update()
    {
        if (playerInsideTriggerArea)
        {
            if (!updatedSystem)
            {
                if (OnInteraction != null)
                    OnInteraction(teleportName);

                updatedSystem = true;
            }            

            GetInput();
            PopUpAppears();
        }
        else
        {
            PopUpDisappears();
            updatedSystem = false;
        }

        
    }

    public override void GetInput()
    {
        if (PlayerInputs.instance.PlayerPressedInteractButton())
        {
            StationFunction();
            isCanvasOpen = !isCanvasOpen;
        }

        if (isCanvasOpen && PlayerInputs.instance.PlayerPressedInteractExitButton() && canvasTeleportSelection.activeInHierarchy)
        {
            StationFunction();
            isCanvasOpen = !isCanvasOpen;
        }
    }

    // Interactive pop up disappears
    private void PopUpAppears()
    {
        if (!activated)
        {
            popUp.ShowInteraction();
        }
        else
        {
            popUp.ShowMessage();
        }
    }

    // Interactive pop up disappears
    private void PopUpDisappears()
    {
        popUp.HideAll();
    }

    public override void StationFunction()
    {
        if (!activated && inventory.InventoryContainsItem(darkEssence))
        {
            activated = true;
            popUp.HideAll();
            teleportAudioSource.Play();

            inventory.SubstractItemFromInventory(darkEssence);
            popUp.GetComponent<PopUp>().ShowMessage();

            PlayerInputs.instance.canMove = false;
            animator.SetBool("isActivated", true);
            teleportLight.SetActive(true);
            //SaveSystem.SaveTeleporters();
        }
        else if (!activated && !inventory.InventoryContainsItem(darkEssence))
        {
            //popUp.GetComponent<PopUp>().ShowMessage();

            InvokeOnNotEnoughMaterials();
        }
        else
        {
            if (!isCanvasOpen)
            {
                ActivateTeleportMenu();
            }
            else if (isCanvasOpen)
            {
                DeactivateTeleportMenu();
            }
        }
    }

    public void SetTeleporterActive()
    {
        activated = true;
        PlayerInputs.instance.canMove = true;
        popUp.GetComponent<PopUp>().HideMessage();

        if (OnActivation != null)
            OnActivation(teleportName);
    }

    private void DesactivateSprite()
    {
        teleportSprite.SetActive(false);
    }

    private void ActivateTeleportMenu()
    {
        canvasTeleportSelection.SetActive(true);

        hudGameObject.SetActive(false);

        PlayerInputs.instance.SetInGameMenuOpenInputs();

        if (OnMenuEnter != null)
            OnMenuEnter();
    }

    private void DeactivateTeleportMenu()
    {
        PlayerInputs.instance.SetInGameMenuCloseInputs();

        hudGameObject.SetActive(true);
        canvasTeleportSelection.SetActive(false);

        //if (OnMenuExit != null)
        //    OnMenuExit();
    }
}
